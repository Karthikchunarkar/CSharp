using System.Net.WebSockets;
using System.Globalization;
using d3e.core;
using gqltosql.schema;
using store;
using Classes;
using Microsoft.CSharp.RuntimeBinder;
using security;
using list;
using models;
using gqltosql;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using IKVM.Runtime;

namespace rest.ws
{
    public class D3EWebsocket
    {
        private const long SESSION_TIMEOUT = 15_000;
        private static readonly long RECONNECT_TIMEOUT = 20 * 60 * 1000;
        public static readonly int ERROR = 0;
        public const int CONFIRM_TEMPLATE = 1;
        public const int HASH_CHECK = 2;
        public const int TYPE_EXCHANGE = 3;
        public const int RESTORE = 4;
        public const int OBJECT_QUERY = 5;
        public const int DATA_QUERY = 6;
        public const int SAVE = 7;
        public const int DELETE = 8;
        public const int UNSUBSCRIBE = 9;
        public const int LOGIN = 10;
        public const int LOGIN_WITH_TOKEN = 11;
        public const int CONNECT = 12;
        public const int LOGOUT = 14;
        public const int OBJECTS = -1;
        public const int CHANNEL_MESSAGE = -2;
        public const int CHANNEL_MESSAGE_ACK = -3;
        public const int RPC_MESSAGE = -4;


        private TemplateManager _templateManager;

        private TransactionWrapper _wrapper;

        private EntityHelperService _helperService;

        private JwtTokenUtil _jwtTokenUtil;
        private Channels _channels;

        private RPCHandler _rpcHandler;

        private RocketQuery _query;

        private RocketMutation _mutation;

        private IModelSchema _schema;

        private MasterTemplate _master;

        private Dictionary<string, Authenticator> _authenticators = new Dictionary<string, Authenticator>();

        private int _reconnectPeriod;
        private System.Timers.Timer _timer;
        private Dictionary<string, ClientSession> _sessions = new Dictionary<string, ClientSession>();
        private Dictionary<string, ClientSession> _disconnectedSessions;
        private Dictionary<ClientSession, Dictionary<string, IDisposable>> _subscriptions = new Dictionary<ClientSession, Dictionary<string, IDisposable>>();

        public static string ReadableFileSize(long size)
        {
            if (size <= 0)
                return "0";
            string[] units = new string[] { "B", "kB", "MB", "GB", "TB" };
            int digitGroups = (int)(Math.Log10(size) / Math.Log10(1024));
            return digitGroups.ToString("#,##0.#", CultureInfo.InvariantCulture) + " " + units[digitGroups];
        }

        public static void printHeapMemoryStatus()
        {

            GC.Collect();
            GC.WaitForPendingFinalizers();
            long totalMemory = GC.GetTotalMemory(false); // Total allocated memory
            long freeMemory = totalMemory - GC.GetTotalMemory(true); // Approximate free memory
            double usedPercentage = ((double)(totalMemory - freeMemory) / totalMemory) * 100;

            // Log memory status
            Log.Info($"Memory: Total: {ReadableFileSize(totalMemory)}, Free: {ReadableFileSize(freeMemory)}, Used: {usedPercentage:0.00}%");
        }

        public void Init()
        {
            _disconnectedSessions = new Dictionary<string, ClientSession>();

            _timer = new System.Timers.Timer(SESSION_TIMEOUT);

            _timer.Elapsed += (sender, e) =>
            {
                foreach (var item in _sessions.Values)
                {
                    try
                    {
                        if (item.IsLocked())
                        {
                            return;
                        }
                        item.SendPingAsync();
                    }
                    catch (Exception ex) { }
                }
            };
            _schema.GetAllTypes();
            _master.GetTemplateType("");
        }

        //public void RegisterWebSocketHandlers(H)

        public void AfterConnectionEstablished(WebSocket session)
        {
            Log.Info("D3EWebsocket connected. " + session.GetHashCode());
            _sessions[session.GetHashCode().ToString()] = new ClientSession(session, ExecuteMessage());
        }

        public void AfterConnectionClosed(WebSocket session, WebSocketCloseStatus status)
        {
            string sessionid = session.GetHashCode().ToString();
            Log.Info("D3EWebsocket connection closed. " + status + ", " + sessionid);
            _sessions.Remove(sessionid);
            if (_sessions.TryGetValue(sessionid, out var cs))
            {
                _disconnectedSessions[sessionid] = cs;
                cs.SetSession(null);
                _timer.Elapsed += (sender, e) => CleanSession(sessionid);
            }

        }

        protected void CleanSession(string id)
        {
            Log.Info("Closing connection after timeout. " + id);
            _disconnectedSessions.Remove(id);
            if (!_disconnectedSessions.ContainsKey(id))
            {
                return;
            }
            if (_disconnectedSessions.TryGetValue(id, out var cs))
            {
                Dictionary<string, IDisposable> map = _subscriptions[cs];
                if (map != null)
                {
                    foreach (var item in map.Values)
                    {
                        try
                        {
                            item.Dispose();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
                try
                {
                    _wrapper.DoInTransaction(() =>
                    {
                        if (cs.UserId != 0)
                        {
                            CurrentUser.Set(cs.UserType, cs.UserId, id);
                        }
                        else
                        {
                            //HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());
                        }
                        _channels.DisConnect(cs);
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        //protected void handleBinaryMessage(WebSocket session)
        //{
        //    ClientSession cs = _sessions[session.GetHashCode().ToString()];
        //    if (cs == null)
        //    {
        //        Log.Info("Session was closed. Ignoring message. (It should not happen)");
        //        return;
        //    }
        //    // Write the payload to the session's stream
        //    byte[] bytes = session.CloseStatusDescription.ToArray();
        //    cs.Stream.Write(buffer, 0, result.Count);
        //}

        private void executeMessage(ClientSession cs, RocketMessage reader)
        {
            try
            {
                _wrapper.DoInTransaction(() =>
                {
                    bool hasUser = cs.UserId != 0;
                    if (hasUser)
                    {
                        CurrentUser.Set(cs.UserType, cs.UserId, cs.SessionId);
                    }
                    else
                    {
                        //SecurityContextHolder.getContext().setAuthentication(null);
                    }
                    try
                    {
                        OnMessage(cs, reader);
                    }
                    finally
                    {
                        reader.Flush();
                    }
                    // Log.info("Done");
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (cs.IsLocked())
                {
                    //				reader.flush();
                }
                CurrentUser.Clear();
            }
        }

        private void OnMessage(ClientSession ses, RocketMessage msg)
        {
            int id = msg.ReqId;
            if (id == CHANNEL_MESSAGE)
            {
                // Log.info("Rocket Message: -2");
                msg.WriteInt(CHANNEL_MESSAGE_ACK);
                OnChannelMessage(ses, msg);
                return;
            }
            msg.WriteInt(id);
            int type = msg.ReadByte();
            // Log.info("Rocket Message: " + type);
            switch (type)
            {
                case CONFIRM_TEMPLATE:
                    OnConfirmTemplate(ses, msg);
                    break;
                case HASH_CHECK:
                    OnHashCheck(ses, msg);
                    break;
                case TYPE_EXCHANGE:
                    OnTypeExchange(ses, msg);
                    break;
                case RESTORE:
                    OnRestore(ses, msg);
                    break;
                case OBJECT_QUERY:
                    OnObjectQuery(ses, msg);
                    break;
                case DATA_QUERY:
                    OnDataQuery(ses, msg);
                    break;
                case SAVE:
                    OnSave(ses, msg);
                    break;
                case DELETE:
                    OnDelete(ses, msg);
                    break;
                case UNSUBSCRIBE:
                    OnUnsubscribe(ses, msg);
                    break;
                case LOGIN:
                    OnLogin(ses, msg);
                    break;
                case CONNECT:
                    OnChannelConnect(ses, msg);
                    break;
                case LOGIN_WITH_TOKEN:
                    OnLoginWithToken(ses, msg);
                    break;
                case LOGOUT:
                    OnLogout(ses, msg);
                    break;
                case RPC_MESSAGE:
                    OnRPCMessage(ses, msg);
                    break;
                default:
                    msg.WriteByte(ERROR);
                    msg.WriteString("Unsupported type: " + type);
                    break;
            }
        }

        private void OnLogin(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(LOGIN);
            int usage = msg.ReadInt();
            Log.Info("Login: usage " + usage);
            string type = msg.ReadString();
            string email = msg.ReadString();
            string phone = msg.ReadString();
            string username = msg.ReadString();
            string password = msg.ReadString();
            string deviceToken = msg.ReadString();
            string token = msg.ReadString();
            string code = msg.ReadString();
            try
            {
                Authenticator auth = this._authenticators[type];
                LoginResult res = auth.login(email, phone, username, password, deviceToken, token, code, ses.GetClientAddress());
                if (res.Success)
                {
                    ses.UserId = res.UserObjec.getId();
                    ses.UserType = res.UserObject.getClass().getSimpleName();
                    ses.Token = res.Token;
                    ses.DeviceToken = deviceToken;
                }
                msg.WriteByte(0);
                TemplateUsage usageType = ses.Template.GetUsageType(usage);
                new RocketObjectDataFetcher(msg, ses.GetTemplate(), (t) => FromTypeAndId(t)).Fetch(usageType, res);
            }
            catch (RuntimeBinderException e)
            {
                msg.WriteByte(1);
                msg.WriteStringList((List<string>)e.Data);
            }
            catch (Exception e)
            {
                msg.WriteByte(1);
                msg.WriteStringList(new List<string>() { e.Message });
                Console.WriteLine(e.StackTrace);
            }
        }

        private void OnLoginWithToken(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(LOGIN_WITH_TOKEN);
            Log.Info("Login With Token");
            string token = msg.ReadString();
            msg.ReadString();
            UserProxy userProxy = _jwtTokenUtil.ValidateToken(token);
            if (userProxy != null)
            {
                CurrentUser.set(userProxy.Type, userProxy.UserId, ses.SessionId);
                BaseUser baseUser = CurrentUser.Get();
                try
                {
                    baseUser.isIsActive();
                    ses.UserId = userProxy.UserId;
                    ses.UserType = userProxy.Type;
                    ses.Token = userProxy.Token;
                    msg.WriteByte(0);
                    RocketInputContext ctx = new RocketInputContext(_helperService, ses.Template, msg);
                    ctx.WriteString(userProxy.Token);
                    ctx.WriteRef(baseUser);
                }
                catch (Exception e)
                {
                    msg.WriteByte(1);
                }
            }
            else
            {
                msg.WriteByte(1);
            }
        }

        private void OnLogout(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(LOGOUT);
            Console.WriteLine("Logout");
            var user = CurrentUser.Get();
            if (user == null || user is AnonymousUser)
            {
                msg.WriteByte(1);
            }
            else
            {
                if (ses.DeviceToken != null)
                {
                    var userDeviceRequest = new UserDevicesRequest
                    {
                        User = user,
                        Token = ses.DeviceToken
                    };
                    var devices = QueryProvider.Get().GetUserDevices(userDeviceRequest).Items;
                    Database.Get().DeleteAll(devices);
                }
                CurrentUser.Clear();
                ses.Logout();
                msg.WriteByte(0);
            }
        }

        private void OnRPCMessage(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(RPC_MESSAGE);
            int clsIdx = msg.ReadInt();
            int methodIdx = msg.ReadInt();
            var tc = ses.Template.GetRPCMethod(clsIdx);
            var message = tc.Methods[methodIdx];
            if (message == null)
            {
                msg.WriteByte(1);
                msg.WriteString("Method not found");
                return;
            }
            int msgSrvIdx = message.Index;
            var ctx = new RocketInputContext(_serviceProvider.GetService<EntityHelperService>(), ses.Template, msg);
            try
            {
                _rpcHandler.Handle(clsIdx, msgSrvIdx, ctx, msg);
            }
            catch (Exception e)
            {
                msg.WriteByte(1);
                msg.WriteString(e.Message);
                Console.WriteLine(e);
            }
        }

        private void OnChannelMessage(ClientSession ses, RocketMessage msg)
        {
            int chIdx = msg.ReadInt();
            int msgIndex = msg.ReadInt();
            var tc = ses.Template.GetChannel(chIdx);
            var message = tc.Methods[msgIndex];
            if (message == null)
            {
                msg.WriteByte(1);
                return;
            }
            int msgSrvIdx = message.Index;
            var ctx = new RocketInputContext(_helperService, ses.Template, msg);
            try
            {
                _channels.OnMessage(tc.Clazz, msgSrvIdx, ses, ctx, _helperService);
                msg.WriteByte(0);
            }
            catch (Exception e)
            {
                msg.WriteByte(1);
                msg.WriteString(e.Message);
                Console.WriteLine(e);
            }
        }

        private void OnChannelConnect(ClientSession ses, RocketMessage msg)
        {
            Console.WriteLine("Connecting to channel");
            msg.WriteByte(CONNECT);
            int chIdx = msg.ReadInt();
            var tc = ses.Template.GetChannel(chIdx);
            var dm = tc.Clazz;
            try
            {
                bool result = _channels.Connect(dm, ses, _helperService);
                Console.WriteLine("Channel connect result: " + result);
                if (result)
                {
                    msg.WriteByte(0);
                }
                else
                {
                    msg.WriteByte(1);
                    msg.WriteString("Connection refused");
                }
            }
            catch (Exception e)
            {
                msg.WriteByte(1);
                msg.WriteString(e.Message);
                Console.WriteLine(e);
            }
        }

        private void OnUnsubscribe(ClientSession ses, RocketMessage msg)
        {
            string subId = msg.ReadString();
            if (_subscriptions.TryGetValue(ses, out var map))
            {
                if (map.Remove(subId, out var subscription))
                {
                    try
                    {
                        subscription.Dispose();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        private void OnDelete(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(DELETE);
            int type = msg.ReadInt();
            try
            {
                var tt = ses.Template.GetType(type);
                long id = msg.ReadLong();
                Console.WriteLine("Delete: " + tt.Model.Type + ", id: " + id);
                var obj = FromTypeAndId(new TypeAndId(tt.Model.GetIndex(), id));
                if (obj is CreatableObject creatableObj)
                {
                    _mutation.Delete(creatableObj, false);
                    msg.WriteByte(0);
                }
                else
                {
                    msg.WriteByte(1);
                    msg.WriteStringList(new List<string> { "Must be Creatable Object" });
                }
            }
            catch (ValidationFailedException e)
            {
                msg.WriteByte(1);
                msg.WriteStringList(e.GetErrors());
                throw new Exception("Delete failed: " + string.Join(", ", e.GetErrors()));
            }
            catch (Exception e)
            {
                msg.WriteByte(1);
                msg.WriteStringList(new List<string> { e.Message });
                Console.WriteLine(e);
                throw new Exception("Delete failed");
            }
        }

        public void SendChanges(ClientSession session, Dictionary<DBObject, BitArray> objects)
        {
            var msg = session.CreateMessage();
            msg.WriteInt(OBJECTS);
            msg.WriteBoolean(true);
            var template = session.Template;
            int count = objects.Count;
            msg.WriteInt(count);
            foreach (var entry in objects)
            {
                var key = entry.Key;
                WriteObject(msg, template, key, entry.Value);
            }
            msg.Flush();
        }

        public void SendEmbeddedChanges(ClientSession session, Dictionary<DBObject, Dictionary<DField<object, object>, BitArray>> objects)
        {
            var msg = session.CreateMessage();
            msg.WriteInt(OBJECTS);
            msg.WriteBoolean(true);
            var template = session.Template;
            int count = objects.Count;
            msg.WriteInt(count);
            foreach (var entry in objects)
            {
                var parent = entry.Key;
                var embeddeds = entry.Value;
                int typeIdx = template.ToClientTypeIdx(parent._TypeIdx());
                msg.WriteInt(typeIdx);
                msg.WriteLong(parent.GetId());
                var type = template.GetType(typeIdx);
                foreach (var embedded in embeddeds)
                {
                    var em = (DBObject)embedded.Key.GetValue(parent);
                    int cfid = type.ToClientIdx(embedded.Key.Index);
                    msg.WriteInt(cfid);
                    WriteObject(msg, template, em, embedded.Value);
                }
                msg.WriteInt(-1);
            }
            msg.Flush();
        }

        private void WriteObject(RocketMessage msg, Template template, DBObject obj, BitArray fields)
        {
            int serverType = obj._TypeIdx();
            int typeIdx = template.ToClientTypeIdx(serverType);
            var type = template.GetType(typeIdx);
            msg.WriteInt(typeIdx);
            if (!type.Model.IsEmbedded())
            {
                msg.WriteLong(obj.GetId());
            }
            foreach (var b in fields)
            {
                int cidx = type.ToClientIdx(b);
                if (cidx == -1) continue;
                var f = type.GetField(cidx);
                if (f is UnknownField) continue;
                if (f.GetType() == FieldType.InverseCollection || f.GetType() == FieldType.PrimitiveCollection || f.GetType() == FieldType.ReferenceCollection)
                {
                    var val = f.GetValue(obj);
                    msg.WriteInt(cidx);
                    WriteCompleteList(msg, template, (List<object>)val, f);
                }
                else
                {
                    var val = f.GetValue(obj);
                    if (f.Reference != null && f.Reference.IsEmbedded() && val == null) continue;
                    msg.WriteInt(cidx);
                    WriteChangeVal(msg, template, f, val);
                }
            }
            msg.WriteInt(-1);
        }

        private void WriteCompleteList(RocketMessage msg, Template template, List<object> newColl, DField<object, object> field)
        {
            msg.WriteInt(newColl.Count);
            foreach (var val in newColl)
            {
                if (field.GetType() == FieldType.PrimitiveCollection)
                {
                    msg.WritePrimitiveField(val, field, template);
                }
                else if (val is DFile file)
                {
                    RocketInputContext.WriteDFile(msg, template, file);
                }
                else
                {
                    int type;
                    long id;
                    if (val is TypeAndId typeAndId)
                    {
                        type = typeAndId.Type;
                        id = typeAndId.Id;
                    }
                    else
                    {
                        var dbObj = (DBObject)val;
                        type = dbObj._TypeIdx();
                        id = dbObj.GetId();
                    }
                    int clientType = template.ToClientTypeIdx(type);
                    msg.WriteInt(clientType);
                    if (!field.Reference.IsEmbedded())
                    {
                        msg.WriteLong(id);
                    }
                    msg.WriteInt(-1);
                }
            }
        }

        //private void WriteListChanges(RocketMessage msg, Template template, List<Change> changes, DField<object, object> field)
        //{
        //    // Log.info("List Changes: " + changes.size() + ", " + changes);
        //    msg.WriteInt(-changes.Count);
        //    foreach (Change change in changes)
        //    {
        //        if (change.GetType() == ListChanges.ChangeType.Added)
        //        {
        //            // Log.info("Added At: " + change.index);
        //            msg.WriteInt(change. + 1);
        //            if (field.GetType() == FieldType.PrimitiveCollection)
        //            {
        //                msg.WritePrimitiveField(change.Obj, field, template);
        //            }
        //            else
        //            {
        //                object value = change.Obj;
        //                DBObject dbObj = null;
        //                if (value is TypeAndId val)
        //                {
        //                    dbObj = FromTypeAndId(val);
        //                }
        //                else
        //                {
        //                    dbObj = (DBObject)value;
        //                }
        //                int clientType = template.ToClientTypeIdx(dbObj._TypeIdx());
        //                msg.WriteInt(clientType);
        //                if (!field.Reference.IsEmbedded())
        //                {
        //                    msg.WriteLong(dbObj.Id);
        //                }
        //                msg.WriteInt(-1);
        //            }
        //        }
        //        else
        //        {
        //            // Log.info("Removed At: " + change.index);
        //            msg.WriteInt(-(change.Index + 1));
        //        }
        //    }
        //}

        private void WriteChangeVal(RocketMessage msg, Template template, DField<object, object> field, Object value)
        {
            if (field.GetType() == FieldType.Primitive)
            {
                msg.WritePrimitiveField(value, field, template);
            }
            else if (field.GetType() == FieldType.Reference)
            {
                if (value == null)
                {
                    msg.WriteNull();
                }
                else
                {
                    if (value is TypeAndId)
                    {
                        TypeAndId ti = (TypeAndId)value;
                        int typeIdx = template.ToClientTypeIdx(ti.Type);
                        msg.WriteInt(typeIdx);
                        if (!field.Reference.IsEmbedded())
                        {
                            msg.WriteLong(ti.Id);
                        }
                        msg.WriteInt(-1);
                    }
                    else if (value is DFile)
                    {
                        DFile file = (DFile)value;
                        RocketInputContext.WriteDFile(msg, template, file);
                        return;
                    }
                    else
                    {
                        DBObject dbObj = (DBObject)value;
                        if (!field.Reference.IsEmbedded())
                        {
                            int typeIdx = template.ToClientTypeIdx(dbObj._TypeIdx());
                            msg.WriteInt(typeIdx);
                            msg.WriteLong(dbObj.GetId());
                            msg.WriteInt(-1);
                        }
                        else
                        {
                            WriteObject(msg, template, dbObj, dbObj._Changes().Changes);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Unsupported type. " + value.GetType());
            }
        }

        private DBObject FromTypeAndId(TypeAndId ti)
        {
            string type = _schema.GetType(ti.Type).GetType();
            return _helperService.Get(type, ti.Id);
        }


        private void OnSave(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(SAVE);
            RocketInputContext ctx = new RocketInputContext(_helperService, ses.Template, msg);
            TemplateType tt = ctx.ReadType();
            DatabaseObject obj = ctx.ReadObject<DatabaseObject>(tt);
            Log.Info("Save: " + obj._Type() + ", LID: " + obj.GetLocalId() + ", ID: " + obj.Id);
            try
            {
                if (obj is CreatableObject)
                {
                    _mutation.Save((CreatableObject)obj);
                    List<long[]> localIds = new List<long[]>();
                    obj.UpdateMasters(a =>
                    {
                        if (a.Id == 0l)
                        {
                            a.SetId(a.GetLocalId());
                        }
                        else if (a.GetLocalId() != 0l)
                        {
                            localIds.Add(
                                    new long[] { ses.Template.ToClientTypeIdx(a._TypeIdx()), a.GetLocalId(), a.Id });
                        }
                        a.SetLocalId(0);
                    });
                    if (obj.Id == 0l)
                    {
                        obj.SetId(obj.GetLocalId());
                    }
                    else if (obj.GetLocalId() != 0l)
                    {
                        localIds.Add(
                                new long[] { ses.Template.ToClientTypeIdx(obj._TypeIdx()), obj.GetLocalId(), obj.Id });
                    }
                    obj.SetLocalId(0);
                    msg.WriteByte(0);
                    msg.WriteInt(localIds.Count);
                    localIds.ForEach((v) =>
                    {
                        msg.WriteInt((int)v[0]);
                        msg.WriteLong((int)v[1]);
                        msg.WriteLong((int)v[2]);
                    });
                    ctx.WriteObject(obj);
                }
                else
                {
                    msg.WriteByte(1);
                    msg.WriteStringList(new List<string>() { "Only creatable objects can be saved" });
                }
            }
            catch (ValidationFailedException e)
            {
                msg.WriteByte(1);
                msg.WriteStringList(e.GetErrors());
                throw new Exception("Save failed: " + string.Join(", ", e.GetErrors()), e);
            }
            catch (Exception e)
            {
                msg.WriteByte(1);
                msg.WriteStringList(new List<string> { e.Message});
                throw new Exception("Save failed", e);
            }
        }

        private void OnObjectQuery(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(OBJECT_QUERY);
            int type = msg.ReadInt();
            TemplateType tt = ses.Template.GetType(type);
            bool subscribed = msg.ReadBoolean();
            int usageId = msg.ReadInt();
            Log.Info("Object Query: type: " + type + " usage " + usageId);
            TemplateUsage usage = ses.Template.GetUsageType(usageId);
            RocketInputContext ctx = new RocketInputContext(_helperService, ses.Template, msg);
            try
            {

                QueryResult queryRes = _query.ExecuteOperation("get" + tt.Model.GetType() + "ById",
                        ConvertToField(usage, ses.Template), ctx, subscribed, ses);
                msg.WriteByte(0);
                if (subscribed)
                {
                    IDisposable sub = queryRes.ChangeTracker;
                    string subId = NewSubId();

                    Dictionary<string, IDisposable> map = _subscriptions.[ses];
                    if (map == null)
                    {
                        map = new Dictionary<string, IDisposable>();
                        _subscriptions[ses] = map;
                    }
                    map[subId] = sub;
                    msg.WriteString(subId);
                }
                WriteQueryResult(queryRes, usage, ses.Template, msg);
            }
            catch (InternalException e)
            {
                msg.WriteByte(1);
                msg.WriteStringList(new List<string>() { "Internal Server Error" });
            }
            catch (ValidationFailedException e)
            {
                msg.WriteByte(1);
                msg.WriteStringList(e.GetErrors());
            }
            catch (Exception e)
            {
                msg.WriteByte(1);
                msg.WriteStringList(new List<string> { e.Message});
                Console.WriteLine(e.StackTrace);
            }
        }

        private Field ConvertToField(TemplateUsage tu, Template template)
        {
            if (tu.Field != null)
            {
                return tu.Field;
            }
            Field f = new Field();
            f.Selections = CreateSelections(tu.Types, template);
            tu.Field = f;
            return f;
        }

        private void WriteQueryResult(QueryResult res, TemplateUsage usage, Template template, RocketMessage msg)
        {
            if (res.External)
            {
                new RocketObjectDataFetcher(msg, template, (t) => FromTypeAndId(t)).Fetch(usage, res.Value);
            }
            else
            {
                new RocketOutObjectFetcher(template, msg).Fetch(usage, res.Value);
            }
        }

        private void OnDataQuery(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(DATA_QUERY);
            string query = msg.ReadString();
            bool subscribed = msg.ReadBoolean();
            int usage = msg.ReadInt();
            TemplateUsage tu = ses.Template.GetUsageType(usage);
            Log.Info("Data Query: " + query + ", usage " + usage);
            RocketInputContext ctx = new RocketInputContext(_helperService, ses.Template, msg);
            try
            {
                Field field = ConvertToField(tu, ses.Template);
                QueryResult res = this._query.ExecuteOperation("get" + query, field, ctx, subscribed, ses);
                msg.WriteByte(0);
                if (subscribed)
                {
                    IDisposable sub = res.ChangeTracker;
                    string subId = null;
                    if (sub != null)
                    {
                        subId = NewSubId();
                        Dictionary<string, IDisposable> map = _subscriptions[ses];
                        if (map == null)
                        {
                            map = new Dictionary<string, IDisposable>();
                            _subscriptions[ses] = map;
                        }
                        map[subId] = sub;
                    }
                    msg.WriteString(subId);
                }
                WriteQueryResult(res, tu, ses.Template, msg);
            }
            catch (InternalException e)
            {
                msg.WriteByte(0);
                if (subscribed)
                {
                    msg.WriteString(null);
                }
                WriteDataQueryErrorResult(tu, ses.Template, new List<string> { "\"Internal Server Error\"" }, msg);
            }
            catch (ValidationFailedException e)
            {
                msg.WriteByte(0);
                if (subscribed)
                {
                    msg.WriteString(null);
                }
                WriteDataQueryErrorResult(tu, ses.Template, e.GetErrors(), msg);
            }
            catch (Exception e)
            {
                msg.WriteByte(0);
                if (subscribed)
                {
                    msg.WriteString(null);
                }
                WriteDataQueryErrorResult(tu, ses.Template, new List<string> { e.Message }, msg);
            }
        }

        private void WriteDataQueryErrorResult(TemplateUsage tu, Template template, List<string> errors,
            RocketMessage msg)
        {
            QueryResult res = new QueryResult();
            UsageType type = tu.Types[0];
            TemplateType tt = template.GetType(type.Type);
            DModel<object> model = tt.Model;
            res.Value = model.NewInstance();
            res.Type = model.GetType();
            res.External = true;
            model.GetField("status").SetValue(res.Value, ResultStatus.Errors);
            model.GetField("errors").SetValue(res.Value, errors);
            WriteQueryResult(res, tu, template, msg);
        }

        private string NewSubId()
        {
            return Guid.NewGuid().ToString();
        }

        private Field VonvertToField(TemplateUsage tu, Template template)
        {
            if (tu.Field != null)
            {
                return tu.Field;
            }
            Field f = new Field();
            f.Selections = CreateSelections(tu.Types, template);
            tu.Field = f;
            return f;
        }

        private List<Selection> CreateSelections(UsageType[] types, Template template)
        {
            List<Selection> selections = new List<Selection>();
            foreach (UsageType ut in types)
            {
                Selection selection = CreateSelection(ut, template);
                if (selection != null)
                {
                    selections.Add(selection);
                }
            }
            return selections;
        }

        private Selection CreateSelection(UsageType ut, Template template)
        {
            TemplateType type = template.GetType(ut.Type);
            if (!type._valid)
            {
                return null;
            }
            List<Field> fields = new List<Field>();
            foreach (UsageField uf in ut.Fields)
            {
                try
                {
                    DField<object, object> field = type.GetField(uf.Field);
                    if (field is UnknownField)
                    {
                        continue;
                    }
                    Field f = new Field();
                    f.FieldVar = field;
                    f.Selections = CreateSelections(uf.Types, template);
                    fields.Add(f);
                }
                catch (Exception e)
                {
                    Log.Info("Unknown field: " + ut.Type + ", " + uf.Field);
                }
            }
            return new Selection(type.Model, fields);
        }

        private void OnRestore(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(RESTORE);
            String sessionId = msg.ReadString();
            Log.Info("Restore: " + sessionId);
            _disconnectedSessions.Remove(sessionId);
            if (_disconnectedSessions.TryGetValue(sessionId, out ClientSession disSes))
            {
                if (disSes == null)
                {
                    msg.WriteByte(1);
                }
            }
            else
            {
                msg.WriteByte(0);
                msg.WriteString(ses.GetSessionId()); // New id
                _sessions[ses.GetSessionId()] = disSes;
                try
                {
                    disSes.SetSession(ses.Session);
                }
                catch (IOException e)
                {
                    msg.WriteByte(1);
                    Console.WriteLine(e.StackTrace);
                    return;
                }
            }
        }

        private void OnConfirmTemplate(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(CONFIRM_TEMPLATE);
            msg.WriteString(ses.GetSessionId());
            String templateHash = msg.ReadString();
            long timeOut = msg.ReadLong();
            if (timeOut < 0)
            {
                ses.TimeOut = RECONNECT_TIMEOUT;
            }
            else
            {
                ses.TimeOut = timeOut < RECONNECT_TIMEOUT ? timeOut : RECONNECT_TIMEOUT;
            }
            if (_templateManager.HasTemplate(templateHash))
            {
                ses.Template = _templateManager.GetTemplate(templateHash);
                msg.WriteByte(0);
                Log.Info("Template matched: " + templateHash);
            }
            else
            {
                msg.WriteByte(1);
                Log.Info("Template not matched: " + templateHash);
            }
        }

        private void OnHashCheck(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(HASH_CHECK);
            int types = msg.ReadInt();
            int usages = msg.ReadInt();
            int channels = msg.ReadInt();
            int rpcs = msg.ReadInt();
            ses.Template = new Template(types, usages, channels, rpcs);

            // Types
            Log.Info("Registering types: " + types);
            List<int> unknownTypes = new List<int>();
            for (int i = 0; i < types; i++)
            {
                string typeHash = msg.ReadString();
                TemplateType tt = _master.GetTemplateType(typeHash);
                if (tt == null)
                {
                    unknownTypes.Add(i);
                }
                else
                {
                    ses.Template.SetTypeTemplate(i, tt);
                }
            }
            Log.Info("Unknown types: " + unknownTypes.Count);

            // Usages
            Log.Info("Registering usages: " + usages);
            List<int> unknownUsages = new List<int>();
            for (int i = 0; i < usages; i++)
            {
                string usageHash = msg.ReadString();
                TemplateUsage ut = _master.GetUsageTemplate(usageHash);
                ses.Template.SetUsageTemplate(i, ut);
                if (ut == null)
                {
                    unknownUsages.Add(i);
                }
            }
            Log.Info("Unknown usages: " + unknownUsages.Count);

            // Channels
            Log.Info("Registering channels: " + channels);
            List<int> unknownChannels = new List<int>();
            for (int i = 0; i < channels; i++)
            {
                string channelHash = msg.ReadString();
                TemplateClazz tc = _master.GetChannelTemplate(channelHash);
                if (tc == null)
                {
                    Log.Info("Unknown channel: " + i);
                    unknownChannels.Add(i);
                }
                else
                {
                    ses.Template.SetChannelTemplate(i, tc);
                }
            }
            Log.Info("Unknown channels: " + unknownChannels.Count);

            // RPC
            Log.Info("Registering RPCs: " + rpcs);
            List<int> unknownRPCs = new List<int>();
            for (int i = 0; i < rpcs; i++)
            {
                string rpcHash = msg.ReadString();
                TemplateClazz tc = _master.GetChannelTemplate(rpcHash);
                if (tc == null)
                {
                    Log.Info("Unknown RPC Class: " + i);
                    unknownRPCs.Add(i);
                }
                else
                {
                    ses.Template.SetRPCTemplate(i, tc);
                }
            }
            Log.Info("Unknown : " + unknownRPCs.Count);

            if (unknownTypes.IsNullOrEmpty() && unknownUsages.IsNullOrEmpty() && unknownChannels.IsNullOrEmpty())
            {
                msg.WriteByte(0);
                ComputeTemplateMD5AndAddToManager(ses.Template);
            }
            else
            {
                msg.WriteByte(1);
                msg.WriteIntegerList(unknownTypes);
                msg.WriteIntegerList(unknownUsages);
                msg.WriteIntegerList(unknownChannels);
                msg.WriteIntegerList(unknownRPCs);
            }
        }

        private void ComputeTemplateMD5AndAddToManager(Template template)
        {
            List<string> md5 = new List<string>();
            foreach (TemplateType tt in template.Types)
            {
                md5.Add(tt.Hash);
            }
            foreach (TemplateUsage tu in template.Usages)
            {
                md5.Add(tu.Hash);
            }
            foreach (TemplateClazz tc in template.ChannelInfo)
            {
                md5.Add(tc.Hash);
            }
            foreach (TemplateClazz tc in template.RpcInfo)
            {
                md5.Add(tc.Hash);
            }
            template.Hash = MD5Util.Md5(md5);
            _templateManager.AddTemplate(template);
            Log.Info("Template created: " + template.Hash);
        }

        private void OnTypeExchange(ClientSession ses, RocketMessage msg)
        {
            msg.WriteByte(TYPE_EXCHANGE);
            Template template = ses.Template;
            // Types
            int typesCount = msg.ReadInt();
            TemplateType[] templateTypes = new TemplateType[typesCount];
            List<int> unknownTypes = new List<int>();
            Dictionary<int, List<int>> typesWithUnknownFields = new Dictionary<int, List<int>>();
            int[] parents = new int[typesCount];
            for (int i = 0; i < typesCount; i++)
            {
                int idx = msg.ReadInt();
                string type = msg.ReadString();
                int parent = msg.ReadInt();
                parents[i] = parent;
                DModel<object> md = _schema.GetType(type);
                if (md != null)
                {
                    int fieldsCount = msg.ReadInt();
                    TemplateType tt = new TemplateType(md, fieldsCount);
                    tt.ParentClientCount = parent;
                    template.SetTypeTemplate(idx, tt);
                    for (int j = 0; j < fieldsCount; j++)
                    {
                        string field = msg.ReadString();
                        int typeIdx = msg.ReadInt();
                        // TODO check type
                        DField<object, object> df = md.GetField(field);
                        if (df == null)
                        {
                            List<int> unknownFields = typesWithUnknownFields[idx];
                            if (unknownFields == null)
                            {
                                unknownFields = new List<int>();
                                typesWithUnknownFields[idx] = unknownFields;
                            }
                            unknownFields.Add(j);
                            df = new UnknownField(field);
                        }
                        tt.AddField(j, df);
                    }
                    templateTypes[i] = tt;
                }
                else
                {
                    int fieldsCount = msg.ReadInt();
                    TemplateType tt = new TemplateType(md, fieldsCount);
                    tt._valid = false;
                    tt.UnknowName = type;
                    tt.ParentClientCount = parent;
                    for (int j = 0; j < fieldsCount; j++)
                    {
                        string f = msg.ReadString();
                        msg.ReadInt();
                        tt.AddField(j, new UnknownField(f));
                    }
                    template.SetTypeTemplate(idx, tt);
                    templateTypes[i] = tt;
                    unknownTypes.Add(idx);
                }
            }

            // Update Parents
            for (int i = 0; i < typesCount; i++)
            {
                TemplateType tt = templateTypes[i];
                int parent = parents[i];
                if (parent != -1)
                {
                    tt.SetParent(template.GetType(parent));
                }
            }
            for (int i = 0; i < typesCount; i++)
            {
                TemplateType tt = templateTypes[i];
                tt.computeHash();
                if (tt._valid)
                {
                    _master.AddTypeTemplate(tt);
                }
            }
            foreach (TemplateType tt in template.Types)
            {
                // When setting parent above, in the case that parent also has a parent,
                // there is no guarantee that the parent's parentClientCount is correct.
                // So compute the counts here.
                tt.ComputeParentFieldsCount();
            }

            // Usage
            int usageCount = msg.ReadInt();
            for (int i = 0; i < usageCount; i++)
            {
                int idx = msg.ReadInt();
                int types = msg.ReadInt();
                UsageType[] tus = new UsageType[types];
                for (int j = 0; j < types; j++)
                {
                    UsageType ut = CreateUsageType(msg);
                    tus[j] = ut;
                }
                TemplateUsage tu = new TemplateUsage(tus);
                template.SetUsageTemplate(idx, tu);
                _master.AddUsageTemplate(tu, template);
            }

            // Channels
            int channelCount = msg.ReadInt();
            for (int i = 0; i < channelCount; i++)
            {
                int idx = msg.ReadInt();
                string name = msg.ReadString();
                // Check if channel with this name exists
                DClazz channel = _schema.GetChannel(name);
                if (channel != null)
                {
                    //				Log.info("Channel found: " + name);
                    int msgCount = msg.ReadInt();
                    TemplateClazz tc = new TemplateClazz(channel, msgCount);
                    for (int j = 0; j < msgCount; j++)
                    {
                        string messageName = msg.ReadString();
                        DClazzMethod message = channel.GetMethod(messageName);
                        if (message == null)
                        {
                            //						Log.info("Message not found: " + messageName);
                            // TODO: Add empty message and continue

                            // TODO: Do we need this Hash?
                            List<string> md5 = new List<string>();
                            md5.Add(messageName);
                            int argCount = msg.ReadInt();
                            for (int k = 0; k < argCount; j++)
                            {
                                int type = msg.ReadInt();
                                bool collection = msg.ReadBoolean();
                                DModel<object> dm = template.GetType(type).Model;
                                md5.Add(dm.GetType());
                            }
                            tc.AddMethod(j, message);
                            continue;
                        }
                        //					Log.info("Message found: " + messageName);
                        bool paramNotFound = false;
                        int paramCount = msg.ReadInt();
                        for (int k = 0; k < paramCount; k++)
                        {
                            // Getting the parameter types of the method. These are needed for constructing
                            // the hash
                            int type = msg.ReadInt();
                            bool collection = msg.ReadBoolean();
                            TemplateType tt = template.GetType(type);
                            DModel<object> paramType = tt.Model;
                            if (paramType == null)
                            {
                                // TODO: Collect the rest, add empty message
                                Log.Info("Param not found: " + k);
                                paramNotFound = true;
                                break;
                            }

                            if (!paramNotFound)
                            {
                                message.AddParam(k, new DParam(type, collection));
                            }
                        }
                        tc.AddMethod(j, paramNotFound ? null : message);
                    }
                    template.SetChannelTemplate(idx, tc);
                    _master.AddChannelTemplate(tc, template);
                }
                else
                {
                    // Reject completely, or do what type is doing?
                    List<string> md5 = new List<string>();
                    md5.Add(name);
                    int methodsCount = msg.ReadInt();
                    for (int j = 0; j < methodsCount; j++)
                    {
                        string methodName = msg.ReadString();
                        md5.Add(methodName);
                        int paramsCount = msg.ReadInt();
                        for (int k = 0; k < paramsCount; k++)
                        {
                            int type = msg.ReadInt();
                            string f = template.GetType(type).Model.Type;
                            msg.ReadBoolean();
                            md5.Add(f);
                        }
                    }
                    TemplateClazz tt = new TemplateClazz(channel, methodsCount);
                    string hash = MD5Util.Md5(md5);
                    tt.Hash = hash;
                    template.SetChannelTemplate(idx, tt);
                }
            }

            // RPC
            int rpcCount = msg.ReadInt();
            for (int i = 0; i < rpcCount; i++)
            {
                int idx = msg.ReadInt();
                string name = msg.ReadString();
                // Check if RPC Class with this name exists
                DClazz rpcClass = _schema.GetRPC(name);
                if (rpcClass != null)
                {
                    //				Log.info("RPC Class found: " + name);
                    int methodCount = msg.ReadInt();
                    TemplateClazz tc = new TemplateClazz(rpcClass, methodCount);
                    for (int j = 0; j < methodCount; j++)
                    {
                        string rpName = msg.ReadString();
                        DClazzMethod message = rpcClass.GetMethod(rpName);
                        bool paramNotFound = false;

                        if (message == null)
                        {
                            //						Log.info("Remote Procedure not found: " + rpName);
                            // TODO: Add empty message and continue

                            // TODO: Do we need this Hash?
                            List<string> md5 = new List<string>();
                            md5.Add(rpName);
                            int argCount = msg.ReadInt();
                            for (int k = 0; k < argCount; j++)
                            {
                                int type = msg.ReadInt();
                                bool collection = msg.ReadBoolean();
                                DModel<object> dmf = template.GetType(type).Model;
                                md5.Add(dmf.GetType());
                            }
                            int returnType = msg.ReadInt();
                            DModel<object> dm = template.GetType(returnType).Model;
                            md5.Add(dm.GetType());
                            // TODO: Consider collection in this hash?
                            bool returnColl = msg.ReadBoolean();
                            tc.AddMethod(j, message);
                            continue;
                        }
                        else
                        {
                            //						Log.info("Remote Procedure found: " + rpName);
                            int paramCount = msg.ReadInt();
                            for (int k = 0; k < paramCount; k++)
                            {
                                // Getting the parameter types of the method. These are needed for constructing
                                // the hash
                                int type = msg.ReadInt();
                                bool collection = msg.ReadBoolean();
                                TemplateType tt = template.GetType(type);
                                DModel <object> paramType = tt.Model;
                                if (paramType == null)
                                {
                                    // TODO: Collect the rest, add empty message
                                    Log.Info("Param not found: " + k);
                                    paramNotFound = true;
                                    break;
                                }

                                if (!paramNotFound)
                                {
                                    message.AddParam(k, new DParam(type, collection));
                                }
                            }

                            int returnType = msg.ReadInt();
                            bool returnColl = msg.ReadBoolean();
                            message.SetReturnType(returnType, returnColl);
                        }
                        tc.AddMethod(j, paramNotFound ? null : message);
                    }
                    template.SetRPCTemplate(idx, tc);
                    _master.AddRPCTemplate(tc, template);
                }
                else
                {
                    // Reject completely, or do what type is doing?
                    List<string> md5 = new List<string>();
                    md5.Add(name);
                    int methodsCount = msg.ReadInt();
                    for (int j = 0; j < methodsCount; j++)
                    {
                        string methodName = msg.ReadString();
                        md5.Add(methodName);
                        int paramsCount = msg.ReadInt();
                        for (int k = 0; k < paramsCount; k++)
                        {
                            int type = msg.ReadInt();
                            string h = template.GetType(type).Model.Type;
                            msg.ReadBoolean();
                            md5.Add(h);
                        }
                        int returnType = msg.ReadInt();
                        string f = template.GetType(returnType).Model.GetType();
                        msg.ReadBoolean();
                        md5.Add(f);
                    }
                    TemplateClazz tt = new TemplateClazz(rpcClass, methodsCount);
                    string hash = MD5Util.Md5(md5);
                    tt.Hash = hash;
                    template.SetRPCTemplate(idx, tt);
                }
            }

            ComputeTemplateMD5AndAddToManager(ses.Template);
            msg.WriteIntegerList(unknownTypes);
            msg.WriteInt(typesWithUnknownFields.Count);
            foreach (var item in typesWithUnknownFields)
            {
                msg.WriteInt(item.Key);
                msg.WriteIntegerList(item.Value);
            }
        }

        private UsageType CreateUsageType(RocketMessage msg)
        {
            int typeIdx = msg.ReadInt();
            int fieldsCount = msg.ReadInt();
            UsageType ut = new UsageType(typeIdx, fieldsCount);
            for (int j = 0; j < fieldsCount; j++)
            {
                int f = msg.ReadInt();
                int refs = msg.ReadInt();
                UsageType[] tus = new UsageType[refs];
                for (int k = 0; k < refs; k++)
                {
                    UsageType refer = CreateUsageType(msg);
                    tus[k] = refer;
                }
                UsageField uf = new UsageField(f, tus);
                ut.Fields[j] = uf;
            }
            return ut;
        }

        public void SendDelete(ClientSession session, List<TypeAndId> objects)
        {
            // Log.info("Send Deletes: ");
            RocketMessage msg = session.CreateMessage();
            msg.WriteInt(OBJECTS);
            msg.WriteBoolean(false);
            Template template = session.Template;
            int count = objects.Count;
            msg.WriteInt(count);
            foreach (var entry in objects)
            {
                msg.WriteInt(template.ToClientTypeIdx(entry.Type));
                msg.WriteLong(entry.Id);
            }
            msg.Flush();

        }

        private Action<ClientSession, RocketMessage> ExecuteMessage()
        {
            throw new NotImplementedException();
        }
    }
}
