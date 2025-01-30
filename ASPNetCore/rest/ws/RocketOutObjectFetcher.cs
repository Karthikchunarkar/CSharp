using System.Collections;
using gqltosql;
using gqltosql.schema;

namespace rest.ws
{
    public class RocketOutObjectFetcher
    {
        private RocketMessage _msg;
        private Template _template;

        public RocketOutObjectFetcher(Template template, RocketMessage msg)
        {
            this._template = template;
            this._msg = msg;
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
            else if (value is ICollection val)
            {
                List<object> coll = new List<object> { value };
                // Log.info("w coll: " + coll.size());
                _msg.WriteInt(coll.Count);
                coll.ForEach(s => FetchValue(usage, s));
            }
            else if (value is OutObject val1)
            {
                // Log.info("w ref: " + db.getTypes());
                int clientType = val1.GetType();
                int typeIdx = _template.ToClientTypeIdx(clientType);
                _msg.WriteInt(typeIdx);
                _msg.WriteLong(val1.Id);
                foreach (UsageType ut in usage.Types)
                {
                    TemplateType tt = _template.GetType(ut.Type);
                    FetchReferenceInternal(tt, ut, val1);
                }
                _msg.WriteInt(-1);
            }
            else
            {
                throw new Exception("Unsupported type. " + value.GetType());
            }
        }

        private void FetchPrimitive(object value, DField<object, object> df)
        {
            _msg.WritePrimitiveField(value, df, _template);
        }

        private void FetchDFile(OutObject db)
        {
            string id = db.GetString("id");
            if (id == null)
            {
                _msg.WriteString(null);
                return;
            }
            _msg.WriteString(id);
            _msg.WriteString(db.GetString("name"));
            _msg.WriteLong(db.GetLong("size"));
            _msg.WriteString(db.GetString("mimeType"));
        }

        private void FetchReferenceInternal(TemplateType type, UsageType usage, OutObject value)
        {
            if (!value.IsOfType(type.Model.GetIndex()))
            {
                return;
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
                object val = value.Get(df.Name);
                FetchFieldValue(f, val, df);
            }
        }
        private void FetchFieldValue(UsageField field, object value, DField<object, object> df)
        {
            if (value == null)
            {
                switch (df.GetType())
                {
                    case FieldType.InverseCollection:
                    case FieldType.PrimitiveCollection:
                    case FieldType.ReferenceCollection:
                        _msg.WriteInt(0);
                        return;
                    case FieldType.Primitive:
                        switch (df.GetPrimitiveType())
                        {
                            case FieldPrimitiveType.Boolean:
                                _msg.WriteBoolean(false);
                                break;
                            case FieldPrimitiveType.Double:
                                _msg.WriteDouble(0.0);
                                break;
                            case FieldPrimitiveType.Duration:
                                _msg.WriteInt(0);
                                break;
                            case FieldPrimitiveType.Enum:
                                _msg.WriteInt(0);
                                break;
                            case FieldPrimitiveType.Integer:
                                _msg.WriteInt(0);
                                break;
                            case FieldPrimitiveType.String:
                                _msg.WriteString("");
                                break;
                            default:
                                _msg.WriteNull();
                                break;
                        }
                        return;
                    case FieldType.Reference:
                        DModel<object> refer = df.Reference;
                        if (refer != null && refer.IsEmbedded())
                        {
                            _msg.WriteInt(_template.ToClientTypeIdx(refer.GetIndex()));
                            _msg.WriteNull();
                        }
                        else
                        {
                            _msg.WriteNull();
                        }
                        return;
                }

            }
            else if (value is ICollection)
            {
                List<object> coll = (List<object>)value;
                _msg.WriteInt(coll.Count);
                coll.ForEach(x => FetchFieldValue(field, x, df));
            }
            else if (value is OutObject db)
            {
                int typeIdx = _template.ToClientTypeIdx(db.GetType());
                DModel<object> refer = df.Reference;
                if (refer.GetIndex() != SchemaConstants.DFile) 
                {
                    FetchDFile(db);
                    return;
                }
                _msg.WriteInt(typeIdx);
                if (!refer.IsEmbedded())
                {
                    _msg.WriteLong(db.Id);
                }
                UsageType[] types = field.Types;
                foreach (UsageType type in types)
                {
                    TemplateType tt = _template.GetType(type.Type);
                    FetchReferenceInternal(tt, type, db);
                }
                _msg.WriteInt(-1);
            }
        }
    }
}
