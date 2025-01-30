using gqltosql.schema;

namespace rest.ws
{
    public class UnknownField : DField<object, object>
    {
        public UnknownField(string name) : base(null, 0, name,name)
        {
            
        }

        public override object FetchValue(object _this, IDataFetcher fetcher)
        {
            return null;
        }

        public override FieldPrimitiveType GetPrimitiveType()
        {
            return FieldPrimitiveType.None;
        }

        public override FieldType GetType()
        {
            return FieldType.None;
        }

        public override object GetValue(object _this)
        {
            return null;
        }

        public override void SetValue(object _this, object value)
        {
        }
    }
}
