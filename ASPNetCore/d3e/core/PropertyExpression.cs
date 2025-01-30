using gqltosql.schema;
using store;

namespace d3e.core
{
    public class PropertyExpression : Criterion
    {

        private DField<object, object> _df;

        private PropertyExpression _on;

        public PropertyExpression(PropertyExpression on, DField<object, object> df)
        {
            this._on = on;
            this._df = df;
        }

        public PropertyExpression Prop(string field)
        {
            DModel<object> reference = (DModel<object>)_df.Reference;

            // Get the field from the reference
            DField<object, object> df = reference.GetField(field);
            if (df == null)
            {
                ICustomFieldProcessor<object> processor = CustomFieldService.Get().GetProcessor(reference.GetType());
                if (processor != null)
                {
                    df = processor.GetDField(field);
                }
            }
            return new PropertyExpression(this, df);
        }

        public PropertyExpression GetOn()
        {
            return _on;
        }

        public DField<object, object> GetDf()
        {
            return _df;
        }

        public new FieldType GetType()
        {
            return _df.GetType();
        }

        public FieldPrimitiveType GetPrimitiveType()
        {
            return _df.GetPrimitiveType();
        }

        public string Select(Criteria criteria)
        {
            string a;
            switch (_df.GetType())
            {
                case FieldType.InverseCollection:
                case FieldType.Reference:
                case FieldType.ReferenceCollection:
                    return criteria.CreateRefColumn(_df.GetType(), _df.Reference, ToSql(criteria));
                case FieldType.Primitive:
                case FieldType.PrimitiveCollection:
                    return ToSql(criteria);
                default:
                    break;
            }
            return ToSql(criteria);
        }

        public string ToSql(Criteria criteria)
        {
            string a;
            switch (_df.GetType())
            {
                case FieldType.InverseCollection:
                case FieldType.PrimitiveCollection:
                case FieldType.ReferenceCollection:
                    a = criteria.GetAlias(this);
                    break;
                default:
                    a = criteria.GetAlias(this);
                    break;
            }
            return a + "." + _df.ColumnName;

        }
    }
}
