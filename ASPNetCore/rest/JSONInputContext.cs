using System.Collections;
using System.Text.Json.Nodes;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json.Linq;
using d3e.core;
using gqltosql.schema;
using store;

namespace rest
{
    public class JSONInputContext : GraphQLInputContext, IDocumentReader
    {
        private JObject _json;
        private IModelSchema _schema;
        internal static T FromJsonString<T>(string doc, long id, string type, IModelSchema schema) where T : class
        {
            if (doc == null) { return null; }
            try
            {
                JObject json = new JObject();
                json["id"] = id;
                JSONInputContext ctx = new JSONInputContext(json, EntityHelperService.GetInstance(), null, null, schema);
                return ctx.ReadObject<T>(type, true);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static T Parse<T>(string doc, string type, IModelSchema schema) where T : class
        {
            if (doc == null)
            {
                return null;
            }
            try
            {
                JObject json = new JObject(doc);
                JSONInputContext ctx = new JSONInputContext(json, EntityHelperService.GetInstance(), null, null, schema);
                return ctx.ReadObject<T>(type, true);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static IList ParseColl(string doc, string type, IModelSchema schema)
        {
            if (doc == null)
            {
                return null;
            }
            try
            {
                JArray array = new JArray(doc);
                IList result = new List<object>();
                foreach (JObject obj in array)
                {
                    JSONInputContext ctx = new JSONInputContext(obj, EntityHelperService.GetInstance(), null, null, schema);
                    result.Add(ctx.ReadObject<object>(type, true));
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static T ReadToObjFromJson<T>(T obj, string json, string type, IModelSchema schema) where T : DatabaseObject
        {
            if (json == null || type == null)
            {
                return null;
            }

            JObject jsonObj = new JObject(json);
            JSONInputContext ctx = new JSONInputContext(jsonObj, EntityHelperService.GetInstance(), null, null, schema);
            DModel<object> model = schema.GetType(type);
            ctx.ReadObjectProperties(obj, model);
            return obj;
        }

        public static string ToJsonString(DatabaseObject obj, string type, IModelSchema schema)
        {
            if (obj == null)
            {
                return null;
            }
            JSONInputContext ctx = new JSONInputContext(new JObject(), EntityHelperService.GetInstance(), null, null,
                    schema);
            return ctx.ToJsonString(obj, type);
        }

        public string ToJsonString(DatabaseObject obj, string type)
        {
            return GetObjAsJson(type, obj, true).ToString();
        }

        public void WriteBooleanColl(string field, List<bool> coll)
        {
            try
            {
                JArray array = new JArray();
                foreach (var t in coll)
                {
                    array.Add(t);
                }
                _json[field] = array;
            }
            catch (Exception e)
            {
                new Exception(field, e);
            }
        }

        public void WriteDFileColl(string field, List<DFile> coll)
        {
            try
            {
                JArray array = new JArray();
                foreach (DFile t in coll)
                {
                    array.Add(GetDFileJson(t));
                }
                _json[field] = array;
            }
            catch (Exception e)
            {
                new Exception(field, e);
            }
        }

        public void WriteEmbedded<T>(string field, T exists)
        {
            throw new NotImplementedException();
        }

        public void WriteObj(string field, string type, DatabaseObject obj)
        {
            if (obj == null)
            {
                return;
            }
            _json[field] = GetObjAsJson(type, obj, true);
        }

        private JObject GetObjAsJson(string type, DatabaseObject obj, bool writeFull)
        {
            DModel<object> type2 = _schema.GetType(obj._Type());
            string realType = type2.GetType();
            JSONInputContext ctx = CreateWriteContext();
            if (!realType.Equals(type))
            {
                ctx.WriteString("__typeName", realType, type);
            }
            if (ShouldWriteId(type2))
            {
                ctx._json["id"] = obj.GetId();
            }
            if (writeFull)
            {
                ctx.WriteObjProperties(obj, type2);
            }
            return ctx._json;
        }

        public void WriteObjProperties<T>(T _this, DModel<object> type) where T : DatabaseObject
        {
            foreach (var df in type.GetFields())
            {
                FieldType ft = df.GetType();
                object value = df.GetValue(_this);
                if (value == null)
                {
                    continue;
                }
                string field = df.Name;
                switch (ft)
                {
                    case FieldType.Primitive:
                        WritePrimitive(field, value, df);
                        break;
                    case FieldType.PrimitiveCollection:
                        WritePrimitiveColl(field, (List<object>)value, df);
                        break;
                    case FieldType.Reference:
                        if (df.Reference.IsEmbedded())
                        {
                            WriteChild(field, (T)value, df.Reference.GetType());
                        }
                        else if (df.Reference.GetType().Equals("DFile"))
                        {
                            WriteDFile(field, (DFile)value);
                        }
                        else if (df.IsChild())
                        {
                            // Child
                            WriteChild(field, (T)value, df.Reference.GetType());
                        }
                        else
                        {
                            // Reference
                            WriteRef(field, (T)value, df.Reference.GetType());
                        }
                        break;
                    case FieldType.ReferenceCollection:
                        IList list = (List<object>)value;
                        if (df.Reference.GetType().Equals("DFile"))
                        {
                            WriteDFileColl(field, (List<DFile>) list);
                        }
                        else if (df.IsChild())
                        {
                            WriteChildColl<DatabaseObject>(field, (List<DatabaseObject>) list, df.Reference.GetType());
                        }
                        else
                        {
                            WriteChildColl<DatabaseObject>(field, (List<DatabaseObject>)list, df.Reference.GetType());
                        }
                        break;
                    case FieldType.InverseCollection:
                        // TODO
                        break;
                    default:
                        break;
                }
            }
        }

        private void WritePrimitive(string field, object value, DField<object, object> df)
        {
            switch (df.GetPrimitiveType())
            {
                case FieldPrimitiveType.Boolean:
                    WriteBoolean(field, (bool)value, false);
                    break;
                case FieldPrimitiveType.DFile:
                    WriteDFile(field, (DFile)value);
                    break;
                case FieldPrimitiveType.Date:
                    WriteDate(field, (DateOnly)value);
                    break;
                case FieldPrimitiveType.DateTime:
                    WriteDateTime(field, (DateTime)value);
                    break;
                case FieldPrimitiveType.Double:
                    WriteDouble(field, (double)value, 0);
                    break;
                case FieldPrimitiveType.Duration:
                    WriteDuration(field, (TimeSpan)value);
                    break;
                case FieldPrimitiveType.Enum:
                    if (value is string)
                    {
                        WriteString(field, (string)value, null);
                    }
                    else
                    {
                        Enum enm = (Enum)value;
                        string name = enm.ToString();
                        WriteString(field, name, null);
                    }
                    break;
                case FieldPrimitiveType.Integer:
                    WriteInteger(field, (long)value, 0);
                    break;
                case FieldPrimitiveType.String:
                    WriteString(field, (string)value, null);
                    break;
                case FieldPrimitiveType.Time:
                    WriteTime(field, (TimeOnly)value);
                    break;
                default:
                    break;
            }
        }

        private bool ShouldWriteId(DModel<object> type)
        {
            return !type.IsEmbedded();
        }

        private void WritePrimitiveColl(string field, IList value, DField<object, object> df)
        {
            if (value == null)
            {
                return;
            }
            switch (df.GetPrimitiveType())
            {
                case FieldPrimitiveType.Boolean:
                    WriteBooleanColl(field, (List<bool>) value);
                    break;
                case FieldPrimitiveType.DFile:
                    WriteDFileColl(field, (List<DFile>) value);
                    break;
                case FieldPrimitiveType.Date:
                    WriteDateColl(field, (List<DateOnly>)value);
                    break;
                case FieldPrimitiveType.DateTime:
                    WriteDateTimeColl(field, (List<DateTime>) value);
                    break;
                case FieldPrimitiveType.Double:
                    WriteDoubleColl(field, (List<double>)value);
                    break;
                case FieldPrimitiveType.Duration:
                    WriteDurationColl(field, (List<TimeSpan>)value);
                    break;
                case FieldPrimitiveType.Enum:
                    WriteEnumColl<Enum>(field, (List<Enum>)value);
                    break;
                case FieldPrimitiveType.Integer:
                    WriteLongColl(field, (List<long>)value);
                    break;
                case FieldPrimitiveType.String:
                    WriteStringColl(field, (List<string>)value);
                    break;
                case FieldPrimitiveType.Time:
                    WriteTimeColl(field, (List<TimeOnly>)value);
                    break;
                default:
                    break;
            }
        }

        private void WriteDoubleColl(string field, List<double> value)
        {
            try
            {
                JArray array = new JArray();
                foreach (var item in value)
                {
                    array.Add(item.ToString());
                }
                _json[field] = array;
            }
            catch (Exception ex) { throw new Exception(field, ex); }
        }

        private void WriteDateColl(string field, List<DateOnly> value)
        {
            try
            {
                JArray array = new JArray();
                foreach (var item in value)
                {
                    array.Add(item.ToString());
                }
                _json[field] = array;
            }
            catch (Exception ex) { throw new Exception(field, ex); }    
        }

        public void WriteLongColl(string field, List<long> coll)
        {
            try
            {
                JArray array = new JArray();
                foreach (var item in coll)
                {
                    array.Add(item);
                }
                _json[field] = array;
            }
            catch (Exception ex) { throw new Exception(field, ex); }

        }

        public void WriteStringColl(string field, List<string> coll)
        {
            try
            {
                JArray array = new JArray();
                foreach (var item in coll)
                {
                    array.Add(item);
                }
                _json[field] = array;
            }
            catch (Exception ex) { throw new Exception(field, ex); }
        }

        public void writeDoubleColl(String field, List<double> coll)
        {
            try
            {
                JArray array = new JArray();
                foreach (double t in coll)
                {
                    array.Add(t);
                }
                _json[field] = array;
            }
            catch (Exception e)
            {
                throw new Exception(field, e);
            }
        }

        public void writeDateColl(String field, List<DateOnly> coll)
        {
            try
            {
                JArray array = new JArray();
                foreach (var t in coll)
                {
                    array.Add(t.ToString());
                }
                _json[field] = array;
            }
            catch (Exception e)
            {
                throw new Exception(field, e);
            }
        }

        public void WriteDateTimeColl(string field, List<DateTime> coll)
        {
            try
            {
                JArray array = new JArray();
                foreach (DateTime t in coll)
                {
                    array.Add(t.ToString());
                }
                _json[field] = array;
            }
            catch (Exception e)
            {
                throw new Exception(field, e);
            }
        }

        public void WriteTimeColl(string field, List<TimeOnly> coll)
        {
            try
            {
                JArray array = new JArray();
                foreach (TimeOnly t in coll)
                {
                    array.Add(t.ToString());
                }
                _json[field] = array;
            }
            catch (Exception e)
            {
                throw new Exception(field, e);
            }
        }

        public void WriteDurationColl(string field, List<TimeSpan> coll)
        {
            try
            {
                JArray array = new JArray();
                foreach (TimeSpan t in coll)
                {
                    array.Add(t.TotalMilliseconds);
                }
                _json[field] = array;
            }
            catch (Exception e)
            {
                throw new Exception(field, e);
            }
        }

        public void WriteChildColl<T>(string field, List<T> coll, string type) where T : DatabaseObject
        {
            try
            {
                JArray array = new JArray();
                foreach (var item in coll)
                {
                    JSONInputContext ctx = CreateWriteContext();
                    array.Add(ctx.GetObjAsJson(type, item, true));
                }
                _json[field] = array;
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        public void WriteUnionColl<T>(string field, List<T> coll) where T : DatabaseObject
        {
            try
            {
                JArray array = new JArray();
                foreach (var item in coll)
                {
                    JObject union = new JObject();
                    string type = item.GetType().Name;
                    union["__typeName"] = type;
                    JSONInputContext ctx = CreateWriteContext();
                    union["value" + type] = ctx.GetObjAsJson(type, item, true);
                    array.Add(union);
                }
                _json[field] = array;
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        public void WriteEnumColl<T>(string field, List<T> coll) where T : System.Enum
        {
            try
            {
                JArray array = new JArray();
                foreach (var item in coll)
                {
                    array.Add(item.GetDisplayName());
                }
                _json[field] = array;
            }
            catch (Exception e) { throw new Exception( field, e); }
        }

        public void WriteRefColl<T>(string field, List<T> coll, string type) where T : DatabaseObject
        {
            if (coll.Count == 0)
            {
                return;
            }
            try
            {
                JArray array = new JArray();
                foreach (var item in coll)
                {
                    string realType = item.GetType().Name;
                    JSONInputContext ctx = CreateWriteContext();
                    if (!realType.Equals(type))
                    {
                        ctx.WriteString("__typeName", realType, type);
                    }
                    ctx.WriteLong("id", item.Id, 0);
                    array.Add(ctx._json);
                }
                _json[field] = array;
            }
            catch (Exception e) { throw new Exception(field + ": " + e.Message, e); }
        }

        public void WriteChild<T>(string field, T obj, string type) where T : DatabaseObject
        {
            if (obj == null)
            {
                return;
            }
            try
            {
                JSONInputContext ctx = CreateWriteContext();
                _json[field] = ctx.GetObjAsJson(type, obj, true);
            }
            catch (Exception e) { throw new Exception( field + ": " + e.Message, e); };
        }

        public void WriteUnion<T>(string field, T obj) where T : DatabaseObject
        {
            if (obj == null) { return; }
            try
            {
                string type = obj.GetType().Name;
                JSONInputContext ctx = CreateWriteContext();
                JObject union = new JObject();
                union["value" + type] = ctx.GetObjAsJson(type, obj, true);
                _json[field] = union;
            }
            catch (Exception ex) { throw new Exception( field, ex ); }
        }

        public void WriteRef<T>(string field, T obj, string type) where T : DatabaseObject
        {
            if (obj == null)
            {
                return;
            }
            JSONInputContext ctx = CreateWriteContext();
            _json[field] = ctx.GetObjAsJson(type, obj, false);
        }

        private JSONInputContext CreateWriteContext()
        {
            JSONInputContext ctx = new JSONInputContext(new JObject(), HelperService, null, null, _schema);
            return ctx;
        }

        protected override JSONInputContext CreateContext(string field)
        {
            try
            {
                if (_json[field] == null || _json[field].Type == JTokenType.Null)
                {
                    return null;
                }
                JObject obj = _json[field] as JObject;
                if (obj == null)
                {
                    return null;
                }

                // Create and return a new JSONInputContext
                return CreateReadContext(obj);
            }
            catch (Exception e) { throw new Exception(e.StackTrace); }
        }

        public override List<long> ReadLongColl(string field)
        {
            try
            {
                JArray array = _json[field] as JArray;
                int length = array.Count;
                List<long> res = new List<long>();
                for (int i = 0; i < length; i++)
                {
                    res.Add(array[i].ToObject<long>());
                }
                return res;
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        public override List<string> ReadStringColl(string field)
        {
            try
            {
                JArray array = _json[field] as JArray;
                int length = array.Count;
                List<string> res = new List<string>();
                for (int i = 0; i < length; i++)
                {
                    res.Add(array[i].ToObject<string>());
                }
                return res;
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        public override List<long> ReadIntegerColl(string field)
        {
            try
            {
                JArray array = _json[field] as JArray;
                int length = array.Count;
                List<long> res = new List<long>();
                for (int i = 0; i < length; i++)
                {
                    res.Add(array[i].ToObject<long>());
                }
                return res;
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        public override List<T> ReadChildColl<T>(string field, string type)
        {
            try
            {
                JArray array = _json[field] as JArray;
                int length = array.Count;
                List<T> res = new List<T>();
                for (int i = 0; i < length; i++)
                {
                    JSONInputContext ctx = CreateReadContext(array[i].ToObject<JObject>());
                    res.Add(ctx.ReadObject<T>(type, true));
                }
                return res;
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        protected virtual JSONInputContext CreateReadContext(JObject json)
        {
            return new JSONInputContext(json, HelperService, InputObjectCache, Files, _schema);
        }

        public override List<T> ReadRefColl<T>(string field, string type)
        {
            try
            {
                JArray array = _json[field] as JArray;
                int length = array.Count;
                List<T> res = new List<T>();
                for (int i = 0; i < length; i++)
                {
                    object obj = array[i];
                    if (obj is JObject jObj)
                    {
                        JSONInputContext ctx = CreateReadContext(jObj);
                        res.Add(ctx.ReadObject<T>(type, false));
                    }
                    else
                    {
                        res.Add((T)ReadRef(HelperService.Get(type), array[i].ToObject<long>()));
                    }
                }
                return res;
            }
            catch (Exception e) { throw new Exception(type, e); }
        }

        public override List<T> ReadUnionColl<T>(string field, string type)
        {
            try
            {
                JArray array = _json[field] as JArray;
                int length = array.Count;
                List<T> res = new List<T>();
                for (int i = 0; i < length; i++)
                {
                    string _type = _json["__typeName"].ToObject<string>();
                    JSONInputContext ctx = CreateReadContext(_json["value" + _type].ToObject<JObject>());
                    res.Add(ctx.ReadObject<T>(_type, true));
                }
                return res;
            }
            catch (Exception e) { throw new Exception(type, e); }
        }

        public override List<T> ReadEnumColl<T>(string field) where T : struct 
        {
            try
            {
                JArray array = _json[field] as JArray;
                int length = array.Count;
                List<T> res = new List<T>();
                for (int i = 0; i < length; i++)
                {
                    res.Add(ReadEnumInternal<T>(array[i].ToString()));
                }
                return res;
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        public override List<DFile> ReadDFileColl(string field)
        {
            try
            {
                JArray array = _json[field] as JArray;
                int length = array.Count;
                List<DFile> res = new List<DFile>();
                for (int i = 0; i < length; i++)
                {
                    JSONInputContext ctx = CreateReadContext(array[i].ToObject<JObject>());
                    res.Add(ReadDFileInternal(ctx));
                }
                return res;
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        public override T ReadRef<T>(string field, string type)
        {
            try
            {
                if (_json[field] == null || _json[field].Type == JTokenType.Null)
                {
                    return null;
                }
                JToken obj = _json[field];
                if (obj.Type == JTokenType.Object)
                {
                    GraphQLInputContext ctx = CreateContext(field);
                    return ctx.ReadObject<T>(type, false);
                }
                else if (obj.Type == JTokenType.Null)
                {
                    return default;
                }
                else
                {
                    return (T)ReadRef(HelperService.Get(type), _json[field].ToObject<long>());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading reference", ex);
            }
        }

        public override T ReadChild<T>(string field, string type)
        {
            GraphQLInputContext ctx = CreateContext(field);
            if (ctx == null)
            {
                return default;
            }
            return ctx.ReadObject<T>(type, true);
        }

        public override T ReadUnion<T>(string field, string type)
        {
            try
            {
                if (_json[field] == null || _json[field].Type == JTokenType.Null)
                {
                    return default;
                }
                string _type = _json["__typeName"].ToString();
                JSONInputContext ctx = CreateReadContext(_json["value" + _type].ToObject<JObject>());
                return ctx.ReadObject<T>(_type, true);
            }
            catch (Exception e)
            {
                throw new Exception(type, e);
            }
        }

        public override long ReadLong(string field)
        {
            try
            {
                if (_json[field] == null || _json[field].Type == JTokenType.Null)
                {
                    return 0;
                }
                return _json[field].ToObject<long>();
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        public override string ReadString(string field)
        {
            try
            {
                if (_json[field] == null || _json[field].Type == JTokenType.Null)
                {
                    return default;
                }
                return _json[field].ToObject<string>();
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        public override bool Has(string field)
        {
            return _json[field] == null || _json[field].Type == JTokenType.Null;
        }

        public override long ReadInteger(string field)
        {
            try
            {
                if (_json[field] == null || _json[field].Type == JTokenType.Null)
                {
                    return 0;
                }
                return _json[field].ToObject<long>();
            }
            catch (Exception e) { return 0; }
        }

        public override double ReadDouble(string field)
        {
            try
            {
                if (_json[field] == null || _json[field].Type == JTokenType.Null)
                {
                    return 0.0;
                }
                return _json[field].ToObject<double>();
            }
            catch (Exception e) { return 0.0; }

        }

        public override bool ReadBoolean(string field)
        {
            try
            {
                if (_json[field] == null || _json[field].Type == JTokenType.Null)
                {
                    return false;
                }
                return _json[field].ToObject<bool>();
            }
            catch (Exception e) { return false; }
        }

        public override TimeSpan ReadDuration(string field)
        {
            throw new NotImplementedException();
        }

        public override DateTime ReadDateTime(string field)
        {
            try
            {
                object obj = _json[field];
                return ReadDateTimeFromObj(obj);
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        private DateTime ReadDateTimeFromObj(object obj)
        {
            if (obj is string val)
            {
                return DateTime.Parse(val);
            }
            else
            {
                return new DateTime();
            }
        }

        public override TimeOnly ReadTime(string field)
        {
            try
            {
                object obj = _json[field];
                return ReadTimeFromObj(obj);
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        private TimeOnly ReadTimeFromObj(object obj)
        {
            if (obj is string val)
            {
                return TimeOnly.Parse(val);
            }
            else
            {
                return new TimeOnly();
            }
        }

        public override DateOnly ReadDate(string field)
        {
            try
            {
                object obj = _json[field];
                return ReadDateFromObj(obj);
            }
            catch (Exception e) { throw new Exception(field, e); }
        }

        private DateOnly ReadDateFromObj(object obj)
        {
            if (obj is string val)
            {
                return DateOnly.Parse(val);
            }
            else
            {
                return new DateOnly();
            }
        }

        protected virtual new T ReadObject<T>(string type, bool readFully)
        {
            T obj = base.ReadObject<T>(type, readFully);
            if (obj is DatabaseObject dbObj)
            {
                if (readFully)
                {
                    dbObj._MarkInProxy();
                    ReadObjectProperties(dbObj, _schema.GetType(dbObj._TypeIdx()));
                    dbObj._ClearInProxy();
                }
            }
            return obj;
        }

        public void ReadObjectProperties(object _this, DModel<object> type)
        {
            foreach (var key in _json.Properties())
            {
                DField<object, object> df = type.GetField(key.Name);
                if (df == null || df is DFlatField<object, object>)
                {
                    continue;
                }

                FieldType ft = df.GetType();
                switch (ft)
                {
                    case FieldType.Primitive:
                        ReadPrimitive(df, _this, key.Name);
                        break;
                    case FieldType.PrimitiveCollection:
                        ReadPrimitiveColl(df, _this, key.Name);
                        break;
                    case FieldType.Reference:
                        if (df.Reference.IsEmbedded())
                        {
                            ReadEmbedded(key.Name, df.Reference.Type, df.GetValue(_this));
                        }
                        else if (df.Reference.Type == "DFile")
                        {
                            df.SetValue(_this, ReadDFile(key.Name));
                        }
                        else if (df.IsChild())
                        {
                            JSONInputContext ctx = CreateContext(key.Name);
                            df.SetValue(_this, ctx.ReadObject<object>(df.Reference.Type, true));
                        }
                        else
                        {
                            df.SetValue(_this, ReadRef<object>(key.Name, df.Reference.Type));
                        }
                        break;
                    case FieldType.ReferenceCollection:
                        IList colls;
                        if (df.Reference.Type == "DFile")
                        {
                            colls = ReadDFileColl(key.Name);
                        }
                        else if (df.IsChild())
                        {
                            colls = ReadChildColl<object>(key.Name, df.Reference.Type);
                        }
                        else
                        {
                            colls = ReadRefColl<object>(key.Name, df.Reference.Type);
                        }
                        df.SetValue(_this, colls);
                        break;
                    case FieldType.InverseCollection:
                        // TODO
                        break;
                    default:
                        break;
                }
            }
        }

        private void ReadPrimitiveColl(DField<object, object> df, object _this, string field)
        {
            object converted;
            switch (df.GetPrimitiveType())
            {
                case FieldPrimitiveType.Boolean:
                    converted = ReadBooleanColl(field);
                    break;
                case FieldPrimitiveType.DFile:
                    converted = ReadDFileColl(field);
                    break;
                case FieldPrimitiveType.Date:
                    converted = ReadDateColl(field);
                    break;
                case FieldPrimitiveType.DateTime:
                    converted = ReadDateTimeColl(field);
                    break;
                case FieldPrimitiveType.Double:
                    converted = ReadDoubleColl(field);
                    break;
                case FieldPrimitiveType.Duration:
                    converted = ReadDurationColl(field);
                    break;
                case FieldPrimitiveType.Enum:
                    converted = ReadEnumColl(field, df.Reference);
                    break;
                case FieldPrimitiveType.Integer:
                    converted = ReadIntegerColl(field);
                    break;
                case FieldPrimitiveType.String:
                    converted = ReadStringColl(field);
                    break;
                case FieldPrimitiveType.Time:
                    converted = ReadTimeColl(field);
                    break;
                default:
                    throw new Exception("Unsupported type. " + df.GetPrimitiveType());
            }
            df.SetValue(_this, converted);
        }

        private List<TimeSpan> ReadDurationColl(string field)
        {
            try
            {
                JArray array = _json[field] as JArray;
                if (array == null)
                {
                    throw new InvalidOperationException($"Field '{field}' is not a JSON array.");
                }

                List<TimeSpan> res = new List<TimeSpan>();
                foreach (var item in array)
                {
                    long val = item.ToObject<long>();
                    res.Add(ReadDuration(val));
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading duration collection", ex);
            }
        }

        private List<double> ReadDoubleColl(string field)
        {
            try
            {
                JArray array = _json[field] as JArray;
                if (array == null)
                {
                    throw new InvalidOperationException($"Field '{field}' is not a JSON array.");
                }

                List<double> res = new List<double>();
                foreach (var item in array)
                {
                    res.Add(item.ToObject<double>());
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading double collection", ex);
            }
        }

        private List<DateTime> ReadDateTimeColl(string field)
        {
            try
            {
                JArray array = _json[field] as JArray;
                if (array == null)
                {
                    throw new InvalidOperationException($"Field '{field}' is not a JSON array.");
                }

                List<DateTime> res = new List<DateTime>();
                foreach (var item in array)
                {
                    res.Add(ReadDateTimeFromObj(item));
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading DateTime collection", ex);
            }
        }

        private List<DateOnly> ReadDateColl(string field)
        {
            try
            {
                JArray array = _json[field] as JArray;
                if (array == null)
                {
                    throw new InvalidOperationException($"Field '{field}' is not a JSON array.");
                }

                List<DateOnly> res = new List<DateOnly>();
                foreach (var item in array)
                {
                    res.Add(ReadDateFromObj(item));
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading Date collection", ex);
            }
        }

        private List<TimeOnly> ReadTimeColl(string field)
        {
            try
            {
                JArray array = _json[field] as JArray;
                if (array == null)
                {
                    throw new InvalidOperationException($"Field '{field}' is not a JSON array.");
                }

                List<TimeOnly> res = new List<TimeOnly>();
                foreach (var item in array)
                {
                    res.Add(ReadTimeFromObj(item));
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading Time collection", ex);
            }
        }

        private List<bool> ReadBooleanColl(string field)
        {
            try
            {
                JArray array = _json[field] as JArray;
                if (array == null)
                {
                    throw new InvalidOperationException($"Field '{field}' is not a JSON array.");
                }

                List<bool> res = new List<bool>();
                foreach (var item in array)
                {
                    res.Add(item.ToObject<bool>());
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading boolean collection", ex);
            }
        }

        private TimeSpan ReadDuration(long millis)
        {
            if (millis == 0)
            {
                return new TimeSpan();
            }
            return TimeSpan.FromMicroseconds(millis);
        }

        private object ReadEnumColl(string field, DModel<object> enumType)
        {
            try
            {
                JArray? array = _json[field].ToObject<JArray>();
                int length = array.Count;
                IList res = new List<object>();
                for (int i = 0; i < length; i++)
                {
                    string val = array[i].ToObject<string>();
                    res.Add(ReadEnum(enumType, val));
                }
                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }

        public override T ReadEmbedded<T>(string field, string type, T exists)
        {
            JSONInputContext ctx = CreateContext(field);
            if (ctx == null)
            {
                return default;
            }
            return ctx.ReadObject<T>(type, true);
        }

        private void ReadPrimitive(DField<object, object> df, object _this, string field)
        {
            object converted;
            switch (df.GetPrimitiveType())
            {
                case FieldPrimitiveType.Boolean:
                    converted = ReadBoolean(field);
                    break;
                case FieldPrimitiveType.DFile:
                    converted = ReadDFile(field);
                    break;
                case FieldPrimitiveType.Date:
                    converted = ReadDate(field);
                    break;
                case FieldPrimitiveType.DateTime:
                    converted = ReadDateTime(field);
                    break;
                case FieldPrimitiveType.Double:
                    converted = ReadDouble(field);
                    break;
                case FieldPrimitiveType.Duration:
                    converted = ReadDuration(field);
                    break;
                case FieldPrimitiveType.Enum:
                    converted = ReadEnum(field, df.Reference);
                    break;
                case FieldPrimitiveType.Integer:
                    converted = ReadInteger(field);
                    break;
                case FieldPrimitiveType.String:
                    converted = ReadString(field);
                    break;
                case FieldPrimitiveType.Time:
                    converted = ReadTime(field);
                    break;
                default:
                    throw new Exception("Unsupported type. " + df.GetPrimitiveType());
            }
            df.SetValue(_this, converted);
        }

        private object ReadEnum(string field, DModel<object> enumType)
        {
            string name = _json[field].ToString();
            if (string.IsNullOrEmpty(name))
            {
                return null; // Return null if the field is missing or empty
            }
            return ReadEnum(enumType, name);
        }

        private object ReadEnum(DModel<object> type, string name)
        {
            // Get the field corresponding to the enum name
            DField<object, object> field2 = type.GetField(name);
            if (field2 == null)
            {
                throw new InvalidOperationException($"Enum field '{name}' not found in type '{type}'.");
            }
            // Return the enum value (assuming the field's value is the enum instance)
            return field2.GetValue(null);
        }
        public JSONInputContext(JObject json, EntityHelperService helperService, Dictionary<long, object> inputObjectCache,
            Dictionary<string, DFile> files, IModelSchema schema) : base(helperService, inputObjectCache, files)
        {
            this._json = json;
            this._schema = schema;
        }

        public void WriteEnum<T>(string field, T val, T def) where T : Enum
        {
            if (val == null || val.Equals(def))
            {
                return;
            }
            _json[field] = val.ToString();
        }

        public void WriteLong(string field, long val, long def)
        {
            if (val == def)
            {
                return;
            }
            _json[field] = val;
        }

        public void WriteString(string field, string val, string def)
        {
            if (val == def)
            {
                return;
            }
            _json[field] = val;
        }

        public void WriteInteger(string field, long val, long def)
        {
            if (val == def)
            {
                return;
            }
            _json[field] = val;
        }

        public void WriteDouble(string field, double val, double def)
        {
            if (val == def)
            {
                return;
            }
            _json[field] = val;
        }

        public void WriteBoolean(string field, bool val, bool def)
        {
            if (val == def)
            {
                return;
            }
            _json[field] = val;
        }

        public void WriteDuration(string field, TimeSpan val)
        {
            if (val == null)
            {
                return;
            }
            _json[field] = val.TotalMilliseconds;
        }

        public void WriteDateTime(string field, DateTime val)
        {
            if (val == null)
            {
                return;
            }
            _json[field] = val.ToString("o"); // ISO 8601 format
        }

        public void WriteTime(string field, TimeOnly val)
        {
            if (val == null)
            {
                return;
            }
            _json[field] = val.ToString("T");
        }

        public void WriteDate(string field, DateOnly val)
        {
            if (val == null)
            {
                return;
            }
            _json[field] = val.ToString("d");
        }

        public void WriteDFile(string field, DFile val)
        {
            if (val == null)
            {
                return;
            }
            _json[field] = GetDFileJson(val);
        }

        private JObject GetDFileJson(DFile val)
        {
            JObject obj = new JObject
            {
                ["id"] = val.Id,
                ["size"] = val.GetSize(),
                ["name"] = val.GetName(),
                ["mimeType"] = val.GetMimeType()
            };
            return obj;
        }
    }
}
