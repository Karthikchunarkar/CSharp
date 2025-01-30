using System.Collections;
using Microsoft.OpenApi.Extensions;
using d3e.core;
using gqltosql.schema;
using store;

namespace rest
{
    public abstract class AbstractModelSchema : IModelSchema
    {
        protected Dictionary<string, DModel<object>> _allTypes = new Dictionary<string, DModel<object>>();
        private DModel<object>[] _all;
        private Dictionary<string, DClazz> _allChannels = new Dictionary<string, DClazz>();
        private DClazz[] _channels;
        private Dictionary<string, DClazz> _allRPCs = new Dictionary<string, DClazz>();
        private DClazz[] _rpcs;

        public void Init()
        {
            CreateAllPrimitives();
            CreateAllEnums();
            CreateAllTables();
            AddFields();
            ComputeHierarchyTypes();
            RecordAllChannels();
            RecordAllRPCs();
        }

        protected int GetTotalCount()
        {
            return SchemaConstants._TOTAL_COUNT;
        }

        private void ComputeHierarchyTypes()
        {
            _all = new DModel<object>[GetTotalCount()];
            foreach (var type in _allTypes.Values)
            {
                _all[type.GetIndex()] = type;
            }
            foreach (var item in _all)
            {
                if (item == null)
                {
                    continue;
                }
                ComputeHierarchyType(item);
            }
        }

        protected void ComputeHierarchyType(DModel<object> m)
        {
            List<int> types = new List<int>();
            DModel<object> temp = m;
            while (temp != null)
            {
                types.Insert(0, temp.GetIndex());
                temp = temp.GetParent();
            }
            AddSubTypes(m, types);
        }

        private void AddSubTypes(DModel<object> model, List<int> types)
        {
            foreach (var m in _all)
            {
                if (m != null && m.GetParent() == model)
                {
                    types.Add(m.GetIndex());
                    AddSubTypes(m, types);
                }
            }
        }

        protected abstract void AddFields();

        protected abstract void CreateAllTables();

        protected abstract void CreateAllEnums();

        protected void RecordAllChannels()
        {
        }

        protected void RecordAllRPCs()
        {
        }

        public List<DModel<object>> AllTypes { get { return new List<DModel<object>>(_allTypes.Values); } }

        public new DModel<object> GetType(string type)
        {
            return _allTypes[type];
        }

        public new DModel<T> GetType2<T>(string type)
        {
            if (_allTypes[type] is DModel<T> model)
            {
                return model;
            }
            return null;
        }

        public DModel<object> GetType(int type)
        {
            return _all[type];
        }

        protected void AddTable(DModel<object> model)
        {
            _allTypes[model.GetType()] = model;
        }

        protected void AddEnum(Enum enm, int index)
        {
            Enum[] constants = (Enum[])Enum.GetValues(enm.GetType());
            DModel<object> dm = new DModel<object>(enm.GetDisplayName(), index, constants.Length, 0, null, DModelType.ENUM);
            int idx = 0;
            foreach (var e in constants)
            {
                dm.AddPrimitive(e.GetDisplayName(), idx++, null, FieldPrimitiveType.Enum, t => t, null, null);
            }
            AddTable(dm);
        }

        private void CreateAllPrimitives()
        {
            AddTable(new DModel<object>("void", SchemaConstants.Void, 0, 0, null, DModelType.PRIMITIVE));
            AddTable(new DModel<object>("String", SchemaConstants.String, 0, 0, null, DModelType.PRIMITIVE));
            AddTable(new DModel<object>("Integer", SchemaConstants.Integer, 0, 0, null, DModelType.PRIMITIVE));
            AddTable(new DModel<object>("Double", SchemaConstants.Double, 0, 0, null, DModelType.PRIMITIVE));
            AddTable(new DModel<object>("Boolean", SchemaConstants.Boolean, 0, 0, null, DModelType.PRIMITIVE));
            AddTable(new DModel<object>("Date", SchemaConstants.Date, 0, 0, null, DModelType.PRIMITIVE));
            AddTable(new DModel<object>("DateTime", SchemaConstants.DateTime, 0, 0, null, DModelType.PRIMITIVE));
            AddTable(new DModel<object>("Time", SchemaConstants.Time, 0, 0, null, DModelType.PRIMITIVE));
            AddTable(new DModel<object>("Duration", SchemaConstants.Duration, 0, 0, null, DModelType.PRIMITIVE));
            AddTable(new DModel<object>("Blob", SchemaConstants.Blob, 0, 0, null, DModelType.PRIMITIVE));
            AddTable(new DModel<object>("Geolocation", SchemaConstants.Geolocation, 0, 0, null, DModelType.PRIMITIVE));
        }

        protected void AddDFileFields()
        {
            DModel<DFile> m = GetType2<DFile>("DFile");
            m.AddPrimitive("id", 0, "_id", FieldPrimitiveType.String, (s) => s.Id, (s, v) => s.Id = v, null)
                .NotNull();
            m.AddPrimitive("name", 1, "_name", FieldPrimitiveType.String, (s) => s.GetName(), (s, v) => s.SetName(v), null);
            m.AddPrimitive("size", 2, "_size", FieldPrimitiveType.Integer, (s) => s.GetSize(), (s, v) => s.SetSize(v), null)
                    .NotNull();
            m.AddPrimitive("mimeType", 3, "_mime_type", FieldPrimitiveType.String, (s) => s.GetSize(),
                    (s, v) => s.SetSize(v), null).NotNull();
        }

        protected void RecordNumChannels(int num)
        {
            _channels = new DClazz[num];
        }

        protected void RecordNumRPCs(int num)
        {
            _rpcs = new DClazz[num];
        }

        protected DClazz AddChannel(string name, int index, int numMsgs)
        {
            return AddClazz(name, index, numMsgs, false);
        }

        protected DClazz AddRPCClass(string name, int index, int numMsgs)
        {
            return AddClazz(name, index, numMsgs, true);
        }

        private DClazz AddClazz(string name, int index, int numMsgs, bool rpc)
        {
            DClazz channel = new DClazz(name, index, numMsgs);
            if (rpc)
            {
                _rpcs[index] = channel;
                _allRPCs[name] = channel;
            }
            else
            {
                _channels[index] = channel;
                _allChannels[name] = channel;
            }
            return channel;
        }

        protected void PopulateChannel(DClazz channel, int msgIndex, string msgName, DParam[] values)
        {
            channel.AddMethod(msgIndex, new DClazzMethod(msgName, msgIndex, values));
        }

        protected void populateRPC(DClazz channel, int msgIndex, string msgName, DParam[] values)
        {
            PopulateRPC(channel, msgIndex, msgName, -1, values);
        }

        protected void PopulateRPC(DClazz channel, int msgIndex, string msgName, int returnType, DParam[] values)
        {
            PopulateRPC(channel, msgIndex, msgName, returnType, false, values);
        }

        protected void PopulateRPC(DClazz channel, int msgIndex, string msgName, int returnType, bool returnColl,
                DParam[] values)
        {
            channel.AddMethod(msgIndex, new DClazzMethod(msgName, msgIndex, returnType, returnColl, values));
        }

        public void AssignObjectId(DModel<object> type, DatabaseObject obj, long id)
        {
            throw new NotImplementedException();
        }

        public long CreateCompoundId(int type, long id)
        {
            throw new NotImplementedException();
        }

        public DatabaseObject CreateNewInstance(int type, long id)
        {
            DModel<object> dm = GetType(type);
            DatabaseObject ins = (DatabaseObject)dm.NewInstance();
            ins.SetId(id);
            if (!dm.IsCreatable() && dm.IsDocument())
            {
                ins.SetSaveStatus(DBSaveStatus.Saved);
                return ins;
            }
            MarkCollectionsAsProxy(dm, ins);
            ins.SetSaveStatus(DBSaveStatus.Saved);
            ins.PostLoad();
            ins._MarkProxy();
            return ins;
        }

        private void MarkCollectionsAsProxy(DModel<object> dm, DatabaseObject ins)
        {
            foreach (var df in dm.GetFields())
            {
                FieldType type = df.GetType();
                switch (type)
                {
                    case FieldType.InverseCollection:
                    case FieldType.PrimitiveCollection:
                    case FieldType.ReferenceCollection:
                        IList list = (IList)df.GetValue(ins);
                        if (list is D3EPersistanceList<object>)
                        {
                            ((D3EPersistanceList<object>)list).MarkProxy();
                        }
                        break;
                }
            }
            if (dm.GetParent() != null)
            {
                MarkCollectionsAsProxy(dm.GetParent(), ins);
            }
        }

        public long ExtractCompoundId(long id)
        {
            return id;
        }

        public DBChange FindChanges(DBObject _this)
        {
            return _this._Changes();
        }

        public DModel<object> Get(DBObject obj)
        {
            return GetType(obj._TypeIdx());
        }

        public List<DClazz> GetAllChannels()
        {
            return new List<DClazz>(_channels);
        }

        public List<DClazz> GetAllRPCs()
        {
            return new List<DClazz>(_rpcs);
        }

        public List<DModel<object>> GetAllTypes()
        {
            throw new NotImplementedException();
        }

        public DClazz GetChannel(string name)
        {
            return _allChannels[name];
        }

        public DField<object, object> GetCollectionField(DModel<object> type, D3EPersistanceList<object> list)
        {
            return type.GetField(list.GetField());
        }

        public long GetDatabaseId(DatabaseObject obj)
        {
            return obj.Id;
        }

        public DClazz GetRPC(string name)
        {
            return _allRPCs[name];
        }

        public void MarkInProxy(DatabaseObject obj, bool inProxy)
        {
            if (inProxy)
            {
                obj._MarkProxy();
            }
            else
            {
                obj._ClearInProxy();
                obj._ClearProxy();
            }
        }
    }
}
