using System.Collections;
using System.Collections.Concurrent;
using java.awt;
using d3e.core;
using gqltosql;
using gqltosql.schema;
using list;
using security;
using store;

namespace rest.ws
{
    public class DataChangeTracker
    {
        D3EWebsocket socket;

        IModelSchema schema;

        ObjectFactory<EntityHelperService> helperService;

        D3EEntityManagerProvider emProvider;

        private BlockingCollection<_Event> _eventQueue = new BlockingCollection<_Event>();

        private bool _shutdown = false;

        List<Key> keys = new List<Key>();
        Dictionary<int, Dictionary<long, ObjectInterests>> perObjectListeners = new Dictionary<int, Dictionary<long, ObjectInterests>>();
        Dictionary<int, List<ObjectListener>> perTypeListeners = new Dictionary<int, List<ObjectListener>>();

        private AppSessionProvider _sessionProvider;

        

        public void Init()
        {

        }

        public IDisposable Listen(object obj, Field field, ClientSession session)
        {
            int type = 0;
            long id = 0;
            if (obj is OutObject)
            {
                OutObject outObject = (OutObject)obj;
                type = outObject.GetType();
                id = outObject.Id;
            }
            else if (obj is TypeAndId)
            {
                TypeAndId typeId = (TypeAndId)obj;
                type = typeId.Type;
                id = typeId.Id;
                obj = FromTypeAndId<object>(typeId);
            }
            else
            {
                DBObject dbObj = (DBObject)obj;
                type = dbObj._TypeIdx();
                id = dbObj.GetId();
            }
            TypeAndId typeAndId = new TypeAndId(type, id);
            DisposableListener dl = new DisposableListener(field, typeAndId, session);
            _Event ev = new _Event()
            {
                type = _EventType.Scan,
                dl = dl,
                field = field,
                obj = obj,
                userProxy = _sessionProvider.GetCurrentUserProxy(),
                cache = (D3EPrimaryCache)emProvider.Get().GetCache(),
            };
            _eventQueue.Add(ev);
            return dl;

        }

        public void Run()
        {
            while (!_shutdown)
            {
                try
                {
                    _Event eventObj = _eventQueue.Take(); // Equivalent to Java's eventQueue.take()

                    switch (eventObj.type)
                    {
                        case _EventType.Scan:
                            emProvider.Create(eventObj.cache);
                            _sessionProvider.SetUserProxy(eventObj.userProxy);
                            Scan(null, null, eventObj.obj, eventObj.field, eventObj.dl, null, null, true, new Dictionary<string, object>());
                            emProvider.Clear();
                            break;

                        case _EventType.DisposeTl:
                            DoOnDispose(eventObj.tl);
                            break;

                        case _EventType.DisposeDl:
                            DoOnDispose(eventObj.dl);
                            break;

                        case _EventType.ListenType:
                            DoOnListen(eventObj.tl);
                            break;

                        case _EventType.Fire:
                            emProvider.Create(eventObj.cache);
                            _sessionProvider.SetUserProxy(eventObj.userProxy);
                            ObjectsToSend toSend = new ObjectsToSend();
                            DoFire(toSend, eventObj.obj2, eventObj.changeType, eventObj.changes);
                            toSend.Send(socket);
                            emProvider.Clear();
                            break;

                        case _EventType.Fire2:
                            emProvider.Create(eventObj.cache);
                            _sessionProvider.SetUserProxy(eventObj.userProxy);
                            var events = (List<DataStoreEvent>)eventObj.obj;
                            ObjectsToSend toSend2 = new ObjectsToSend();

                            foreach (var ev in events)
                            {
                                if (ev.GetEntity() is DBObject dbObject)
                                {
                                    DoFire(toSend2, dbObject, ev.GetType(), eventObj.changes);
                                }
                            }

                            toSend2.Send(socket);
                            emProvider.Clear();
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    _sessionProvider.Clear();
                }
            }
        }

        private void DoOnListen(TypeListener tl)
        {
            List<ObjectListener> perType = perTypeListeners[tl.type];
            if (perType == null)
            {
                perType = new List<ObjectListener>();
                perTypeListeners[tl.type] = perType;
            }
            ObjectListener ol = new ObjectListener(tl.fields, tl, null);
            perType.Add(ol);
            tl.types.Add(tl.type);
        }

        private void DoOnDispose(TypeListener tl)
        {
            int type = tl.type;
            List<ObjectListener> fieldListeners = perTypeListeners[type];
            if (fieldListeners == null)
            {
                return;
            }
            fieldListeners.Remove(new ObjectListener(tl.fields, tl, null));
        }

        private void DoOnDispose(DisposableListener dl)
        {
            foreach (var ti in dl.objects)
            {
                Dictionary<long, ObjectInterests> objectListerns = perObjectListeners[ti.Type];
                if (objectListerns != null)
                {
                    ObjectInterests ol = objectListerns[ti.Id];
                    if (ol != null)
                    {
                        foreach (var item in ol.fieldListeners)
                        {
                            if (item.listener is DisposableListener val)
                            {
                                if (val.IsDisposed())
                                {
                                    ol.fieldListeners.Remove(item);
                                }
                            }
                        }
                        if (ol.fieldListeners.Count == 0)
                        {
                            objectListerns.Remove(ti.Id);
                        }
                        foreach (var item in ol.refListeners)
                        {
                            if (item.listener.IsDisposed())
                            {
                                ol.refListeners.Remove(item);
                            }
                        }
                    }
                }
            }

            foreach (var type in dl.types)
            {
                List<ObjectListener> fieldListeners = perTypeListeners[type];
                if (fieldListeners != null)
                {
                    foreach (var item in fieldListeners)
                    {
                        if (item.listener is DisposableListener val)
                        {
                            if (val.IsDisposed())
                            {
                                fieldListeners.Remove(item);
                            }
                        }
                    }
                    if (fieldListeners.Count > 0)
                    {
                        perTypeListeners.Remove(type);
                    }
                }
            }
        }


        public void OnDispose(DisposableListener dl)
        {
            _Event ev = new _Event();
            ev.type = _EventType.DisposeDl;

            ev.dl = dl;
            _eventQueue.Add(ev);
        }
        private void Scan(DBObject parent, DField<object, object> parentField, object obj, Field field, DisposableListener dl,
            ObjectsToSend toSend, List<int> allParents, bool sendAll, Dictionary<DBObject, DBChange> changes)
        {
            int type = 0;
            long id = 0;
            DBObject dbObj = null;
            if (obj is OutObject)
            {
                OutObject outObject = (OutObject)obj;
                type = outObject.GetType();
                id = outObject.Id;
            }
            else if (obj is TypeAndId)
            {
                TypeAndId typeId = (TypeAndId)obj;
                type = typeId.Type;
                id = typeId.Id;
                obj = FromTypeAndId<object>(typeId);
            }
            else
            {
                dbObj = (DBObject)obj;
                type = dbObj._TypeIdx();
                if (obj is StructBase)
                {
                    type = ((StructBase)obj)._actualType();
                }
                id = dbObj.GetId();
            }
            DModel<object> model = this.schema.GetType(type);
            if (model.IsEmbedded())
            {
                return;
            }
            DModel<object> temp = model;
            BitArray fieldSet = new BitArray(model.GetFieldsCount());
            List<Field> fields = new List<Field>();
            while (temp != null)
            {
                Selection sel = field.GetSelectionForType(temp.GetIndex());
                if (sel != null)
                {
                    fieldSet.Or(sel.FieldSet);
                    fields.AddRange(sel.Fields);
                }
                temp = temp.GetParent();
            }
            if (fields.Count == 0)
            {
                return;
            }
            Dictionary<long, ObjectInterests> perObj = perObjectListeners[type];
            if (perObj == null)
            {
                perObj = new Dictionary<long, ObjectInterests>();
                perObjectListeners[type] = perObj;
            }
            ObjectInterests interests = perObj[id];
            if (interests == null)
            {
                interests = new ObjectInterests();
                perObj[id] = interests;
            }
            ObjectListener ol = new ObjectListener(fieldSet, dl, field);
            interests.fieldListeners.Add(ol);
            if (parent == null)
            {
                interests.refListeners.Add(new ObjectUsage(-1, -1, -1, field, dl));
            }
            else
            {
                interests.refListeners
                        .Add(new ObjectUsage(parent._TypeIdx(), parent.GetId(), parentField.Index, field, dl));
            }
            TypeAndId typeAndId = new TypeAndId(type, id);
            bool newObject = sendAll;
            if (!dl.objects.Contains(typeAndId))
            {
                dl.objects.Add(typeAndId);
                newObject = true;
            }
            dl.types.Add(type);
            if (toSend != null && dbObj != null)
            {
                // Found a DBObject. So send it.
                // If the original object was a DataQuery object, this will be called for
                // each item in the DQ object.
                // We cannot check for changes here, since doing so will prevent new objects
                // from being shared across clients.
                if (sendAll)
                {
                    BitArray set = field.GetBitSet(allParents);
                    toSend.Add(dl.session, dbObj, set);
                }
                else
                {
                    DBChange change = changes[dbObj];
                    if (change != null && !change.IsEmpty())
                    {
                        BitArray set = field.GetBitSet(allParents);
                        toSend.Add(dl.session, dbObj, set);
                    }
                }
            }
            //		DModel<?> objType = schema.getType(type);
            //		 Log.info("WATCHING " + SEL.GETTYPE().GETTYPE() + " ID: " + ID + " FIELDS: "
            //		 + LISTEXT.MAP(SEL.GETFIELDS(), (F) -> F.GETField().getName()) + " Object Type: " + objType.getType() + " Type : " + type);
            foreach (Field f in fields)
            {
                DField<object, object> dField = f.FieldVar;
                if (dField.Reference == null || dField.Reference.GetType().Equals("DFile"))
                {
                    continue;
                }
                FieldType fieldType = dField.GetType();
                if (fieldType == FieldType.Reference)
                {
                    object value;
                    if (obj is OutObject)
                    {
                        OutObject outObject = (OutObject)obj;
                        value = outObject.GetFields()[dField.Name];
                    }
                    else
                    {
                        value = dField.GetValue(obj);
                    }
                    if (value != null && !(value is DFile))
                    {
                        if (toSend == null)
                        {
                            Scan(dbObj, dField, value, f, dl, null, null, newObject, changes);
                        }
                        else
                        {
                            DModel<object> dm = dField.Reference;
                            List<int> allParents2 = new List<int>();
                            dm.AddAllParents(allParents2);
                            allParents2.Add(dm.GetIndex());
                            Scan(dbObj, dField, value, f, dl, toSend, allParents2, newObject, changes);
                        }

                    }
                }
                else if (fieldType == FieldType.ReferenceCollection || fieldType == FieldType.InverseCollection)
                {
                    IList value = null;
                    if (obj is OutObject)
                    {

                        OutObject outObject = (OutObject)obj;
                        value = (IList)outObject.GetFields()[dField.Name];
                    }
                    else
                    {
                        value = (IList)dField.GetValue(obj);
                    }
                    if (value != null)
                    {
                        if (toSend == null)
                        {
                            foreach (object o in value)
                            {
                                Scan(dbObj, dField, o, f, dl, null, null, newObject, changes);
                            }
                        }
                        else
                        {
                            DModel<object> dm = dField.Reference;
                            List<int> allParents2 = new List<int>();
                            dm.AddAllParents(allParents2);
                            allParents2.Add(dm.GetIndex());
                            foreach (object o in value)
                            {
                                Scan(dbObj, dField, o, f, dl, toSend, allParents2, newObject, changes);
                            }
                        }
                    }
                }
            }
        }


        private void Remove(DBObject obj, ObjectUsage ou)
        {
            int type = obj._TypeIdx();
            long id = obj.GetId();
            Selection sel = ou.field.GetSelectionForType(type);
            if (sel.Fields.Count == 0)
            {
                return;
            }
            Dictionary<long, ObjectInterests> perObj = perObjectListeners[type];
            if (perObj == null)
            {
                return;
            }
            ObjectInterests interests = perObj[id];
            if (interests == null)
            {
                return;
            }
            BitArray fieldsSet = sel.FieldSet;
            interests.fieldListeners.Remove(new ObjectListener(fieldsSet, ou.listener, ou.field));
            interests.refListeners.Remove(ou);
            // Log.info("Un Watching " + sel.getType().getType() + " ID: " + id + "
            // Fields: "
            // + ListExt.map(sel.getFields(), (f) -> f.getField().getName()));
            foreach (Field f in sel.Fields)
            {
                DField<object, object> dField = f.FieldVar;
                if (dField.Reference == null || dField.Reference.GetType().Equals("DFile"))
                {
                    continue;
                }
                int fieldIndex = dField.Index;
                FieldType fieldType = dField.GetType();
                if (fieldType == FieldType.Reference)
                {
                    DBObject value = (DBObject)dField.GetValue(obj);
                    if (value != null)
                    {
                        List<ObjectUsage> refListeners = RefListeners(value._TypeIdx(), value.GetId(), type, id,
                                fieldIndex);
                        foreach (var ol in refListeners)
                        {
                            Remove(value, ol);
                        }
                    }
                }
                else if (fieldType == FieldType.ReferenceCollection || fieldType == FieldType.InverseCollection)
                {
                    List<object> value = (List<object>)dField.GetValue(obj);
                    if (value != null && !(value.Count == 0))
                    {
                        foreach (object o in value)
                        {
                            DBObject dbObj = null;
                            if (o is TypeAndId)
                            {
                                dbObj = (DBObject?)FromTypeAndId<object>((TypeAndId)o);
                            }
                            else
                            {
                                dbObj = (DBObject)o;
                            }
                            if (dbObj != null)
                            {
                                List<ObjectUsage> refListeners = RefListeners(dbObj._TypeIdx(), dbObj.GetId(), type, id,
                                        fieldIndex);
                                foreach (var ol in refListeners)
                                {
                                    Remove(dbObj, ol);
                                }
                            }
                        }
                    }
                }
            }
        }

        public T FromTypeAndId<T>(TypeAndId ti)
        {
            DModel<object> model = schema.GetType(ti.Type);
            if (model.IsEmbedded())
            {
                return default;
            }
            string type = model.GetType();
            DBObject value = helperService.GetObject().Get(type, ti.Id);
            return (T)value;
        }

        public List<T> FromTypeAndId<T>(List<TypeAndId> list)
        {
            List<T> res = new List<T>();
            list.ForEach(a => res.Add(FromTypeAndId<T>(a)));
            return res;
        }

        public IDisposable Listen(int type, BitArray fields, Action<DBObject, DBObject, StoreEventType> listener)
        {
            TypeListener dl = new TypeListener(type, fields, listener);
            _Event ev = new _Event();
            ev.type = _EventType.ListenType;

            ev.tl = dl;
            _eventQueue.Add(ev);
            return dl;
        }

        private void DoOnlisten(TypeListener dl)
        {
            List<ObjectListener> perType = perTypeListeners[dl.type];
            if (perType == null)
            {
                perType = new List<ObjectListener>();
                perTypeListeners[dl.type] = perType;
            }
            ObjectListener ol = new ObjectListener(dl.fields, dl, null);
            perType.Add(ol);
            dl.types.Add(dl.type);
        }

        public void Fire(DBObject dBObject, StoreEventType changeType)
        {
            _Event ev = new _Event();
            ev.type = _EventType.Fire;

            ev.obj2 = dBObject;
            ev.changeType = changeType;

            ev.userProxy = _sessionProvider.GetCurrentUserProxy();
            ev.cache = (D3EPrimaryCache)emProvider.Get().GetCache();
            Dictionary<DBObject, DBChange> changes = new Dictionary<DBObject, DBChange>();
            changes[dBObject] = PrepareChanges(dBObject);
            if (dBObject is DatabaseObject)
            {

                ((DatabaseObject)dBObject).VisitChildren(a => changes[a] = PrepareChanges(a));
            }
            dBObject._ClearChanges();
            ev.changes = changes;
            _eventQueue.Add(ev);
        }

        private DBChange PrepareChanges(DBObject dBObject)
        {
            DBChange ch = dBObject._Changes();
            if (dBObject is DatabaseObject)
            {
                DatabaseObject db = ((DatabaseObject)dBObject);
                if (db.CanCreateOldObject())
                {
                    db.CreateOldObject();
                }
                ch.Old = db.GetOld();
            }
            return ch;
        }

        private void DoFire(ObjectsToSend toSend, DBObject dBObject, StoreEventType changeType, Dictionary<DBObject, DBChange> changes)
        {
            DBChange ch = changes[dBObject];
            DBObject old = ch == null ? null : ch.Old;
            long id = dBObject.GetId();
            // Log.info(
            // "Fire: " + object._type() + " ID: " + id + " Event:" + changeType.toString()
            // + " Changes: " + ch.changes);
            int type = dBObject._TypeIdx();
            if (dBObject is StructBase)
            {
                type = ((StructBase)dBObject)._actualType();
            }

            bool isDelete = changeType == StoreEventType.Delete;

            Dictionary<long, ObjectInterests> objectListeners = perObjectListeners[type];
            HashSet<ObjectListener> listeners = new HashSet<ObjectListener>();
            ObjectInterests interests = null;
            if (objectListeners != null)
            {
                interests = objectListeners[dBObject.GetId()];
                if (interests != null)
                {
                    foreach (var ol in interests.fieldListeners)
                    {
                        if(ol.listener is DisposableListener val)
                        {
                            if(val.IsDisposed())
                            {
                                // Remove
                            }
                        }
                        else if (ol.fields == null || isDelete || Intersects(ol.fields, ch.Changes))
                        {
                            listeners.Add(ol);
                        }
                    }
                }
            }
            List<ObjectListener> fieldListeners = perTypeListeners[type];
            if (fieldListeners != null)
            {
                foreach (ObjectListener ol in fieldListeners)
                {
                    if (ol.listener is DisposableListener val)
                    {
                        if (val.IsDisposed())
                        {

                            // Remove
                        }
                    }
                    else if (ol.fields == null || Intersects(ol.fields, ch.Changes))
                    {
                        listeners.Add(ol);
                    }
                }
            }
            if (interests != null)
            {
                DModel<object> model = schema.GetType(type);
                foreach (int field in ch.Changes.Stream().ToArrary())
                {
                    DField<object, object> dField = model.GetField(field);
                    int fieldIndex = dField.Index;
                    FieldType fieldType = dField.GetType();
                    DModel<object> refer = dField.Reference;
                    if (refer == null || refer.GetIndex() == SchemaConstants.DFile)
                    {
                        continue;
                    }
                    if (fieldType == FieldType.Reference)
                    {
                        if (dField.Reference.IsEmbedded() && changeType != StoreEventType.Delete)
                        {
                            foreach (ObjectListener ol in interests.fieldListeners)
                            {
                                if (ol.listener is DisposableListener)
                                {
                                    DisposableListener dl = (DisposableListener)ol.listener;
                                    IEnumerable<Field> expand = ol.field.Selections.SelectMany(x => x.Fields);
                                    IEnumerable<Field> where = expand.Where(x => x.FieldVar == dField);
                                    BitArray set = new BitArray(0);
                                    foreach (Field f in where)
                                    {
                                        set.Or(f.GetBitSet(new List<int>() { dField.Reference.GetIndex() }));
                                    }
                                    DBObject em = (DBObject)dField.GetValue(dBObject);
                                    set.And(em._Changes().Changes);
                                    toSend.AddEmbedded(dl.session, dBObject, dField, set);
                                }
                            }
                            continue;
                        }
                        object _oldValue = ch.OldValues[field];
                        object _newValue = dField.GetValue(dBObject);
                        long oldId = 0;
                        if (_oldValue != null && _newValue == null)
                        {
                            DBObject dbObj = null;
                            int oldType = 0;
                            if (_oldValue is TypeAndId)
                            {

                                TypeAndId typeId = (TypeAndId)_oldValue;
                                oldType = typeId.Type;
                                oldId = typeId.Id;
                                dbObj = FromTypeAndId<DBObject>((TypeAndId)_oldValue);
                            }
                            else
                            {
                                dbObj = (DBObject)_oldValue;
                                oldType = dbObj._TypeIdx();
                                oldId = dbObj.GetId();
                            }
                            List<ObjectUsage> refListeners = RefListeners(oldType, oldId, type, id, fieldIndex);
                            if (dbObj != null)
                            {
                                foreach (var ol in refListeners)
                                {
                                    Remove(dbObj, ol);
                                }
                            }
                        }
                        if (_newValue != null)
                        {
                            DBObject dbObj = null;
                            if (_newValue is TypeAndId)
                            {
                                dbObj = FromTypeAndId<DBObject>((TypeAndId)_newValue);
                            }
                            else
                            {
                                dbObj = (DBObject)_newValue;
                            }
                            DModel<object> dm = schema.GetType(dbObj._TypeIdx());
                            List<int> allParents = new List<int>();
                            dm.AddAllParents(allParents);
                            allParents.Add(dm.GetIndex());
                            foreach (ObjectUsage ou in interests.refListeners)
                            {
                                IEnumerable<Field> expand = ou.field.Selections.SelectMany(x => x.Fields);
                                IEnumerable<Field> where = expand.Where(x => x.FieldVar == dField);
                                foreach (Field f in where)
                                {
                                    Scan(dBObject, dField, dbObj, f, ou.listener, toSend, allParents, oldId != dbObj.GetId(),
                                            changes);
                                }
                            }
                        }
                    }
                    else if (fieldType == FieldType.ReferenceCollection || fieldType == FieldType.InverseCollection)
                    {
                        IList _oldValue = (IList)ch.OldValues[field];
                        IList _newValue = (IList)dField.GetValue(dBObject);

                        // Change was not due to List structure change
                        // So, the inner object changed
                        bool childChange = _oldValue == null;
                        List<long> oldIds = new List<long>();
                        if (!childChange)
                        {
                            // If the list structure has not changed, then everything in _oldValue will be
                            // in _newValue
                            foreach (object o in _oldValue)
                            {
                                if (_newValue.Contains(o))
                                {
                                    if (o is TypeAndId)
                                    {
                                        TypeAndId typeId = (TypeAndId)o;
                                        oldIds.Add(typeId.Id);
                                    }
                                    else
                                    {
                                        DBObject val = (DBObject)o;
                                        oldIds.Add(val.GetId());
                                    }
                                    continue;
                                }
                                DBObject dbObj = null;
                                int oldType = 0;
                                long oldId = 0;
                                if (o is TypeAndId)
                                {
                                    TypeAndId typeId = (TypeAndId)o;
                                    oldType = typeId.Type;
                                    oldId = typeId.Id;
                                    dbObj = FromTypeAndId<DBObject>((TypeAndId)o);
                                }
                                else
                                {
                                    dbObj = (DBObject)o;
                                    oldType = dbObj._TypeIdx();
                                    oldId = dbObj.GetId();
                                }
                                oldIds.Add(oldId);
                                List<ObjectUsage> refListeners = RefListeners(oldType, oldId, type, id, fieldIndex);
                                if (dbObj != null)
                                {
                                    foreach (var ol in refListeners)
                                    {
                                        Remove(dbObj, ol);
                                    }
                                }
                            }
                        }
                        foreach (object o in new List<object>() { _newValue })
                        {
                            if (childChange || _oldValue.Contains(o))
                            {
                                if (!(o is DBObject))
                                {
                                    continue;
                                }

                                // Tracking changes in child collection
                                DBObject val1 = (DBObject)o;
                                if (val1._Changes() == null || val1._Changes().Changes.Count == 0)
                                {
                                    continue;
                                }
                            }
                            DBObject dbObj = null;
                            if (o is TypeAndId)
                            {
                                dbObj = FromTypeAndId<DBObject>((TypeAndId)o);
                            }
                            else
                            {
                                dbObj = (DBObject)o;
                            }
                            if (dbObj != null)
                            {
                                DModel<object> dm = schema.GetType(dbObj._TypeIdx());
                                List<int> allParents = new List<int>();
                                dm.AddAllParents(allParents);
                                allParents.Add(dm.GetIndex());
                                foreach (ObjectUsage ou in interests.refListeners)
                                {
                                    IEnumerable<Field> expand = ou.field.Selections.SelectMany(x => x.Fields);
                                    IEnumerable<Field> where = expand.Where(x => x.FieldVar == dField);
                                    foreach (Field f in where)
                                    {
                                        Scan(dBObject, dField, dbObj, f, ou.listener, toSend, allParents,
                                                !oldIds.Contains(dbObj.GetId()), changes);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (listeners.Count == 0)
            {
                return;
            }

            if (isDelete)
            {
                List<ObjectUsage> refListeners = RefListeners(type, id);
                if (refListeners != null)
                {
                    foreach (var refL in refListeners)
                    {
                        Remove(dBObject, refL);
                    }
                }
            }

            foreach (ObjectListener ol in listeners)
            {
                if (ol.listener is DisposableListener)
                {
                    DisposableListener dl = (DisposableListener)ol.listener;
                    if (isDelete)
                    {
                        toSend.Delete(dl.session, new TypeAndId(type, id));
                    }
                    else
                    {
                        BitArray set = new BitArray(0);
                        set.Or(ol.fields);
                        set.And(ch.Changes);
                        toSend.Add(dl.session, dBObject, set);
                    }
                }
                else
                {
                    try
                    {
                        TypeListener tl = (TypeListener)ol.listener;
                        tl.listener(dBObject, old, changeType);
                    }
                    catch (Exception e)
                    {
                        Log.PrintStackTrace(e);
                    }
                }
            }
        }

        public static bool Intersects(this BitArray first, BitArray second)
        {
            if (first == null || second == null || first.Length != second.Length)
                return false;

            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] && second[i])
                    return true;
            }
            return false;
        }
        private List<ObjectUsage> RefListeners(int type, long id)
        {
            Dictionary<long, ObjectInterests> objectListeners = perObjectListeners[type];
            if (objectListeners != null)
            {
                ObjectInterests interests = objectListeners[id];
                if (interests != null)
                {
                    return ListExt.From(interests.refListeners, false);
                }
            }
            return new List<ObjectUsage>();
        }

        private List<ObjectUsage> RefListeners(int type, long id, int parentType, long parentId, int parentFieldIndex)
        {
            Dictionary<long, ObjectInterests> objectListeners = perObjectListeners[type];
            if (objectListeners != null)
            {
                ObjectInterests interests = objectListeners[id];
                if (interests != null)
                {
                    //return ListExt.where(interests.refListeners, ol->ol.parentType == parentType
                    //        && ol.parentId == parentId && ol.fieldIdx == parentFieldIndex);
                }
            }
            return new List<ObjectUsage>();
        }

        public void Fire(List<DataStoreEvent> changes)
        {
            if (changes.Count == 0)
            {
                return;
            }
            _Event ev = new _Event();
            ev.type = _EventType.Fire2;
            Dictionary<DBObject, DBChange> dbChanges = new Dictionary<DBObject, DBChange>();
            foreach (DataStoreEvent ds in changes)
            {
                object value = ds.GetEntity();
                if (value is DBObject)
                {

                    dbChanges[(DBObject)value] = PrepareChanges((DBObject)value);
                }
                if (value is DatabaseObject)
                {
                    ((DatabaseObject)value).VisitChildren(a => dbChanges[a] = PrepareChanges(a));
                }
            }
            ev.changes = dbChanges;
            ev.obj = changes;
            ev.userProxy = _sessionProvider.GetCurrentUserProxy();
            ev.cache = (D3EPrimaryCache)emProvider.Get().GetCache();
            _eventQueue.Add(ev);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}












