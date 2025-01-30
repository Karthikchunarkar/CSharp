using System.Collections;
using com.sun.org.apache.xalan.@internal.xsltc.compiler;
using javax.xml.transform;
using Microsoft.OpenApi.Extensions;
using Classes;
using d3e.core;
using gqltosql.schema;
using store;

namespace rest.ws
{
    public class RocketInputContext
    {
        private RocketMessage _msg;
        Template _template;
        private EntityHelperService _helperService;
        private Dictionary<long, DBObject> localCache = new Dictionary<long, DBObject>();
        private long _localId;

        public Template Template { get => _template; set => _template = value; }

        public RocketInputContext(EntityHelperService helperService, Template template, RocketMessage msg)
        {
            _msg = msg;
            Template = template;
            _helperService = helperService;
        }

        public long ReadLong()
        {
            long l = _msg.ReadLong();
            return l;
        }

        public void WriteObjectList(List<DBObject> list)
        {
            if (list == null)
            {
                return;
            }
            int size = list.Count;
            _msg.WriteInt(size);
            foreach (var obj in list)
            {
                WriteObject((DBObject)obj);
            }
        }

        public void WriteObject(DBObject obj)
        {
            if (obj == null)
            {
                _msg.WriteInt(-1);
                return;
            }
            int typeIdx = Template.ToClientTypeIdx(obj._TypeIdx());
            _msg.WriteInt(typeIdx);
            TemplateType tt = Template.GetType(typeIdx);
            if (!tt.Model.IsEmbedded())
            {
                if (obj.GetId() == 0)
                {
                    obj.SetId(NextId());
                }
                _msg.WriteLong(obj.GetId());
            }
            while (tt != null)
            {
                WriteProperties(obj, tt);
                tt = tt.ParentType;
            }
            _msg.WriteInt(-1);
        }

        private long NextId()
        {
            --_localId;
            return _localId;
        }

        private void WriteProperties(DBObject obj, TemplateType tt)
        {
            DField<object, object>[] fields = tt.Fields;
            int i = tt.ParentClientCount;
            foreach (var df in fields)
            {
                // Log.info("w field: " + i + " " + df.getName());
                if (df is UnknownField)
                {
                    i++;
                    continue;
                }
                FieldType ft = df.GetType();
                object val = df.GetValue(obj);
                if (df.Reference != null && df.Reference.IsEmbedded() && val == null)
                {
                    i++;
                    continue;
                }
                _msg.WriteInt(i++);
                if (val == null)
                {
                    // Log.info("w null: ");
                    _msg.WriteInt(-1);
                    continue;
                }
                switch (ft)
                {
                    case FieldType.Primitive:
                        WritePrimitive(val, df);
                        break;
                    case FieldType.PrimitiveCollection:
                        IList vals = (IList)val;
                        // Log.info("w primitive List: " + vals.size());
                        _msg.WriteInt(vals.Count);
                        foreach (object o in vals)
                        {
                            WritePrimitive(o, df);
                        }
                        break;
                    case FieldType.Reference:
                        DModel<object> refer = df.Reference;
                        if (refer.IsEmbedded())
                        {
                            WriteEmbedded((DBObject)val, df.Reference);
                        }
                        else if (df.IsChild())
                        {
                            WriteObject((DBObject)val);
                        }
                        else if (refer.GetType().Equals("DFile"))
                        {
                            WriteDFile(_msg, Template, (DFile)val);
                        }
                        else
                        {
                            DBObject o = (DBObject) val;
                            if (o.GetId() == 0l)
                            {
                                WriteObject(o);
                            }
                            else
                            {
                                WriteRef(o);
                            }
                        }
                        break;
                    case FieldType.InverseCollection:
                    case FieldType.ReferenceCollection:
                        List<object> coll = new List<object>((int)val);
                        // Log.info("w ref List: " + coll.size());
                        _msg.WriteInt(coll.Count);
                        foreach (object o in coll)
                        {
                            if (df.IsChild())
                            {
                                WriteObject((DBObject)o);
                            }
                            else if (df.Reference.GetType().Equals("DFile"))
                            {
                                WriteDFile(_msg, Template, (DFile)o);
                            }
                            else
                            {
                                DBObject d = (DBObject)o;
                                if (d.GetId() == 0l)
                                {
                                    WriteObject(d);
                                }
                                else
                                {
                                    WriteRef(d);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void WriteEmbedded(DBObject obj, DModel<object> refer)
        {
            if (obj == null)
            {
                _msg.WriteInt(Template.ToClientTypeIdx(refer.GetIndex()));
                _msg.WriteInt(-1);
                return;
            }
            WriteObject(obj);
        }

        public void WriteRef(DBObject obj)
        {
            if (obj == null)
            {
                // Log.info("w ref: null");
                _msg.WriteInt(-1);
                return;
            }
            int typeIdx = Template.ToClientTypeIdx(obj._TypeIdx());
            _msg.WriteInt(typeIdx);
            // Log.info("w ref: " + typeIdx + " " + obj._type());
            if (obj.GetId() == 0l)
            {
                obj.SetId(NextId());
            }
            _msg.WriteLong(obj.GetId());
            _msg.WriteInt(-1);
        }

        private void WritePrimitive(object val, DField<object, object> df)
        {
            _msg.WritePrimitiveField(val, df, Template);
        }

        public static void WriteDFile(RocketMessage _msg, Template template, DFile val)
        {
            _msg.WriteString(val.Id);
            _msg.WriteString(val.GetName());
            _msg.WriteLong(val.GetSize());
            _msg.WriteString(val.GetMimeType());
        }

        private void ReadEmbedded(object obj)
        {
            TemplateType tt = ReadType();
            if (tt == null)
            {
                // Log.info("r emb: null");
                return;
            }
            // Log.info("r emb: " + tt.getModel().getType());
            ReadObjectProperties(obj, tt);
        }

        public List<object> ReadObjectCollection<T>()
        {
            long num = ReadLong();
            IList result = new List<object>();
            for (int i = 0; i < num; i++)
            {
                T obj = ReadObject<T>();
                result.Add(obj);
            }
            return (List<object>)result;
        }

        public T ReadObject<T>()
        {
            TemplateType tt = ReadType();
            return ReadObject<T>(tt);
        }

        public T ReadObject<T>(TemplateType tt)
        {

            if (tt == null)
            {
                // Log.info("r obj: null");
                return default;
            }
            long id = ReadLong();
            // Log.info("r obj: " + id);
            Object obj = null;
            if (id <= 0)
            {
                obj = localCache[id];
            }
            else if (tt.Model.GetModelType() != DModelType.STRUCT)
            {
                EntityHelper<DatabaseObject> entity;
                if (tt.Model.GetExternal() == null)
                {
                    entity = (EntityHelper<DatabaseObject>) _helperService.Get(tt.Model.GetType());
                }
                else
                {
                    entity = _helperService.GetExternalSystem(tt.Model.GetExternal())
                            .GetHelper<DatabaseObject, EntityHelper<DatabaseObject>>(tt.Model.Type);
                }
                obj = entity.GetById(id);
            }
            if (obj == null)
            {
                obj = tt.Model.NewInstance();
                if (obj is DBObject)
                {
                    DBObject dbobj = (DBObject)obj;
                    if (id < 0)
                    {
                        localCache[id] = dbobj;
                    }
                    dbobj.SetLocalId(id);
                }
            }
            ReadObjectProperties(obj, tt);
            return (T)obj;
        }

        private void ReadObjectProperties(object obj, TemplateType tt)
        {
            while (true)
            {
                int fi = _msg.ReadInt();
                if (fi < 0)
                {
                    break;
                }
                DField<object, object> df = tt.GetField(fi);
                // Log.info("r field: " + fi + " " + df.getName());
                FieldType ft = df.GetType();
                df.GetValue(obj); // Just unproxy the object. This will help in creating documented objects
                switch (ft)
                {
                    case FieldType.Primitive:
                        object val = ReadPrimitive(df);
                        df.SetValue(obj, val);
                        break;
                    case FieldType.PrimitiveCollection:
                        IList vals = ReadPrimitiveCollection(df, (IList)df.GetValue(obj));
                        df.SetValue(obj, vals);
                        break;
                    case FieldType.Reference:
                        if (df.Reference.IsEmbedded())
                        {
                            ReadEmbedded(df.GetValue(obj));
                        }
                        else if (df.Reference.GetType().Equals("DFile"))
                        {
                            df.SetValue(obj, ReadDFile());
                        }
                        else
                        {
                            df.SetValue(obj, ReadObject<DFile>());
                        }
                        break;
                    case FieldType.ReferenceCollection:
                        {
                            IList colls;
                            if (df.Reference.GetType().Equals("DFile"))
                            {
                                colls = ReadDFileCollection();
                            }
                            else
                            {
                                colls = ReadReferenceCollection((IList)df.GetValue(obj));
                            }
                            df.SetValue(obj, colls);
                        }
                        break;
                    case FieldType.InverseCollection:
                        throw new Exception("Can not read InverseCollectgion: " + df.Name);
                    default:
                        break;
                }
            }
        }

        private List<T> ReadColl<T>(List<T> old, Func<T> itemReader)
        {
            int size = _msg.ReadInt();
            // Log.info("r coll: " + size);
            if (size < 0)
            {
                // If size is negative, modify the existing list
                old = new List<T>(old); // Create a copy of the old list
                size = -size; // Convert size to positive
                for (int i = 0; i < size; i++)
                {
                    int idx = _msg.ReadInt();
                    if (idx < 0)
                    {
                        // If idx is negative, remove the item at the specified index
                        idx = -idx;
                        idx--;
                        old.RemoveAt(idx);
                    }
                    else
                    {
                        // If idx is positive, add or update the item at the specified index
                        idx--;
                        T obj = itemReader();
                        if (idx >= old.Count)
                        {
                            // If the index is beyond the current size, add the item to the end
                            old.Add(obj);
                        }
                        else
                        {
                            // Otherwise, insert the item at the specified index
                            old.Insert(idx, obj);
                        }
                    }
                }
                return old;
            }
            else
            {
                // If size is non-negative, create a new list and populate it
                List<T> colls = new List<T>();
                for (int i = 0; i < size; i++)
                {
                    colls.Add(itemReader());
                }
                return colls;
            }
        }

        private IList ReadReferenceCollection(IList old)
        {
            return ReadColl((List<IList>) old, () => ReadObject<IList>());
        }

        private IList ReadPrimitiveCollection(DField<object, object> field, IList old)
        {
            return ReadColl((List<object>)old, () => ReadPrimitive(field));
        }

        private object ReadPrimitive(DField<object, object> df)
        {
            return _msg.ReadPrimitive(df, Template);
        }

        public T ReadEnum<T>()
        {
            TemplateType type = ReadType();
            if (type == null)
            {
                // Log.info("r enum: null");
                return default;
            }
            int fidx = _msg.ReadInt();
            DField<object, object> field = type.GetField(fidx);
            // Log.info("r enum: " + field.getName());
            return (T)field.GetValue(null);
        }

        public void WriteEnum<T>(int enumType, T val) where T : Enum
        {
            int clientTypeIdx = Template.ToClientTypeIdx(enumType);
            _msg.WriteInt(clientTypeIdx);
            TemplateType type = Template.GetType(clientTypeIdx);
            DField<object, object> field = type.Model.GetField(val.GetDisplayName());
            int clientIdx = type.ToClientIdx(field.Index);
            _msg.WriteInt(clientIdx);
        }

        public double ReadDouble()
        {
            double d = _msg.ReadDouble();
            // Log.info("r double: " + d);
            return d;
        }

        public List<double> ReadDoubleCollection()
        {
            return ReadColl(new List<double>(), () => ReadDouble());
        }


        public bool ReadBoolean()
        {
            bool b = _msg.ReadBoolean();
            // Log.info("r bool: " + b);
            return b;
        }

        public List<bool> ReadBooleanCollection()
        {
            return ReadColl(new List<bool>(), () => ReadBoolean());
        }

        public DFile ReadDFile()
        {
            return _msg.ReadDFile();
        }

        public List<DFile> ReadDFileCollection()
        {
            return ReadColl( new List<DFile>(), () => ReadDFile());
        }

        public void ReadGeolocationCollection()
        {
            ReadColl( new List<object>(), () => ReadGeolocation());
        }

        public TemplateType ReadType()
        {
            int type = _msg.ReadInt();
            if (type == -1)
            {
                return null;
            }
            return Template.GetType(type);
        }

        public string ReadString()
        {
            string s = _msg.ReadString();
            return s;
        }

        public List<string> ReadStringCollection()
        {
            return ReadColl(new List<string>(), () => ReadString());
        }

        public void WriteStringList(List<string> list)
        {
            _msg.WriteStringList(list);
        }

        public void WriteBoolean(bool val)
        {
            _msg.WriteBoolean(val);
        }

        public void WriteBooleanList(List<bool> list)
        {
            _msg.WriteBooleanList(list);
        }

        public void WriteDFile(DFile val)
        {
            _msg.WriteDFile(val);
        }

        public void WriteString(string str)
        {
            _msg.WriteString(str);
        }

        public void WriteLong(long val)
        {
            _msg.WriteLong(val);
        }

        public void WriteLongList(List<long> val)
        {
            _msg.WriteLongList(val);
        }

        public void WriteDouble(double val)
        {
            _msg.WriteDouble(val);
        }

        public void WriteDoubleList(List<double> list)
        {
            _msg.WriteDoubleList(list);
        }

        public DateOnly ReadLocalDate()
        {
            return _msg.ReadDate();
        }

        public List<DateOnly> ReadLocalDateCollection()
        {
            return ReadColl(new List<DateOnly>(),()=> ReadLocalDate());
        }

        public void WriteLocalDate(DateOnly date)
        {
            _msg.WriteLocalDate(date);
        }

        public void WriteLocalDateList(List<DateOnly> list)
        {
            _msg.WriteLocalDateList(list);
        }

        public DateTime ReadDateTime()
        {
            return _msg.ReadDateTime();
        }

        public DateTime ReadLocalDateTime()
        {
            return ReadDateTime();
        }

        public List<DateTime> ReadLocalDateTimeCollection()
        {
            return ReadColl(new List<DateTime>(), () => ReadLocalDateTime());
        }

        public TimeSpan ReadTime()
        {
            return _msg.ReadTime();
        }

        public TimeSpan ReadLocalTime()
        {
            return ReadTime();
        }

        public Geolocation ReadGeolocation()
        {
            return _msg.ReadGeolocation();
        }

        public List<TimeSpan> ReadLocalTimeCollection()
        {
            return ReadColl(new List<TimeSpan>(), () => ReadLocalTime());
        }

        public TimeSpan ReadDuration()
        {
            long micros = _msg.ReadLong();
            return TimeSpan.FromTicks(micros * 10); // Convert microseconds to ticks
        }

        public List<TimeSpan> ReadDurationCollection()
        {
            return ReadColl(new List<TimeSpan>(), () => ReadDuration());
        }

        public void WriteBlob(Blob changes)
        {
            _msg.WriteBlob(changes);
        }

        public void WriteBlobList(List<Blob> list)
        {
            _msg.WriteBlobList(list);
        }

        public Blob ReadBlob()
        {
            return _msg.ReadBlob();
        }

        public List<Blob> ReadBlobCollection()
        {
            return ReadColl( new List<Blob>(), () => ReadBlob());
        }

        public List<long> ReadLongCollection()
        {
            return ReadColl(new List<long>(), () => ReadLong());
        }

        public void WriteLocalDateTime(DateTime result)
        {
            _msg.WriteLocalDateTime(result);
        }

        public void WriteLocalDateTimeList(List<DateTime> result)
        {
            _msg.WriteLocalDateTimeList(result);
        }

        public void WriteLocalTime(TimeSpan result)
        {
            _msg.WriteLocalTime(result);
        }

        public void WriteLocalTimeList(List<TimeSpan> result)
        {
            _msg.WriteLocalTimeList(result);
        }

        public void WriteDuration(TimeSpan result)
        {
            _msg.WriteDuration(result);
        }

        public void WriteDurationList(List<TimeSpan> result)
        {
            _msg.WriteDurationList(result);
        }

        public void WriteDFileList(List<DFile> result)
        {
            _msg.WriteDFileList(result);
        }

        public void WriteGeolocation(Geolocation result)
        {
            _msg.WriteGeolocation(result);
        }

        public void WriteGeolocationList(List<Geolocation> result)
        {
            _msg.WriteGeolocationList(result);
        }
    }
}
