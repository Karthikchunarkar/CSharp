using System.Collections;
using d3e.core;
using gqltosql.schema;
using list;
using store;

namespace rest.ws
{
    public class RocketObjectDataFetcher
    {
        private RocketMessage _msg;
        private Template _template;
        private Func<TypeAndId, DBObject> _toObject;

        public RocketObjectDataFetcher(RocketMessage msg, Template template, Func<TypeAndId, DBObject> toObject)
        {
            _msg = msg;
            _template = template;
            _toObject = toObject;
        }

        public void Fetch(TemplateUsage usage, object value)
        {
            FetchValue(usage, value);
        }

        private void FetchValue(TemplateUsage usage, object value)
        {
            if (value == null)
            {
                _msg.WriteNull();
            }
            else if (value is DFile)
            {
                _msg.WriteDFile((DFile)value);
            }
            else if (value is string)
            {
                _msg.WriteString((string)value);
            }
            else if (value is long)
            {
                _msg.WriteLong((long)value);
            }
            else if (value is bool)
            {
                _msg.WriteBoolean((bool)value);
            }
            else if (value is Double)
            {
                _msg.WriteDouble((double)value);
            }
            else if (value is ICollection val)
            {
                List<object> coll = (List<object>)val;
                _msg.WriteInt(coll.Count);
                coll.ForEach(v => FetchValue(usage, v));
            }
            else
            {
                FetchReference(usage.Types, (DBObject)value);
            }
        }

        private void FetchReference(UsageType[] types, object value)
        {
            int serverType = 0;
            long id = 0;
            if (value is TypeAndId val)
            {
                serverType = val.Type;
                id = val.Id;
            }
            else
            {
                if (value is DFile file)
                {
                    _msg.WriteDFile(file);
                    return;
                }
                DBObject dbObj = (DBObject)value;
                serverType = dbObj._TypeIdx();
                id = dbObj.Id;
            }
            int typeIdx = _template.ToClientTypeIdx(serverType);
            _msg.WriteInt(typeIdx);
            TemplateType type = _template.GetType(typeIdx);
            if (type.Model.IsEmbedded)
            {
                _msg.WriteLong(id);
            }
            List<int> index = SelectAllTypes(typeIdx);
            foreach (UsageType ut in types)
            {
                if (index.Contains(ut.Type))
                {
                    TemplateType tt = _template.GetType(ut.Type);
                    FetchReferenceInternal(tt, ut, value);
                }
            }
            _msg.WriteInt(-1);
        }

        private List<int> SelectAllTypes(int typeIdx)
        {
            List<int> indxes = new List<int>();
            while (typeIdx != -1)
            {
                TemplateType tt = _template.GetType(typeIdx);
                indxes.Add(typeIdx);
                DModel<object> parent = tt.Model.GetParent();
                if (parent != null)
                {
                    typeIdx = _template.ToClientTypeIdx(parent.GetIndex());
                }
                else
                {
                    typeIdx = -1;
                }
            }
            return indxes;
        }

        private void fetchReferenceInternal(TemplateType type, UsageType usage, object value)
        {
            if (usage.Fields.Length > 0)
            {
                if (value is TypeAndId val)
                {
                    value = _toObject.Invoke(val);
                }
                foreach (UsageField f in usage.Fields)
                {
                    DField<object, object> df = type.GetField(f.Field);
                    if (df is UnknownField)
                    {
                        continue;
                    }
                    // Log.info("w field: " + df.getName());
                    _msg.WriteInt(f.Field);
                    df.FetchValue(value, new DataFetcher(f, df.Reference == null ? 0 : df.Reference.GetIndex(), _msg, _template));
                }
            }
        }

        private class DataFetcher : IDataFetcher
        {

            private RocketMessage _msg;
            private UsageField _field;
            private int _enumType;
            private Template _template;
            public DataFetcher(UsageField field, int enumType, RocketMessage msg, Template template)
            {
                this._field = field;
                this._enumType = enumType;
                this._msg = msg;
                this._template = template;
            }
            public object OnPrimitiveValue<T,R>(object val, DField<object, object> df)
            {
                _msg.WritePrimitiveField(val, df, _template);
                return null;
            }
            public object OnReferenceValue(object value)
            {
                if (value == null)
                {
                    _msg.WriteNull();
                    return null;
                }
                FetchReference(_field.Types, value);
                return null;
            }
            public object OnEmbeddedValue(object value)
            {
                if (value == null)
                {
                    _msg.WriteNull();
                }
                return onReferenceValue(value);
            }
            public object OnPrimitiveList<T,R>(List<object> value, DField<object, object> df)
            {
                // Log.info("w pri List: " + value.size());
                _msg.WriteInt(value.Count);
                value.ForEach(v => onPrimitiveValue(v, df));
                return null;
            }
            public object OnReferenceList<T>(List<object> value)
            {
                // Log.info("w ref List: " + value.size());
                _msg.WriteInt(value.Count);
                value.ForEach(v => onReferenceValue(v));
                return null;
            }
            public object OnFlatValue<T>(List<object> value)
            {
                return onReferenceList(new List<object>(value));
            }

            public object OnInverseValue<T>(List<object> value)
            {
                return onReferenceList(value);
            }
        }
    }
}
