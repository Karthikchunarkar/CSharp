namespace gqltosql.schema
{
    public class DInverseCollField<T, R> : DField<T, List<R>>
    {
        private Func<T, List<R>> _getter;

        public DInverseCollField(DModel<T> decl, int index, string name, string column, DModel<object> refernce, Func<T, List<R>> getter) : base(decl, index, name, column)
        {
            this._getter = getter;
            Reference = refernce;
        }

        public override object FetchValue(T _this, IDataFetcher fetcher)
        {
            return fetcher.OnFlatValue(GetValue(_this));
        }

        public override FieldPrimitiveType GetPrimitiveType()
        {
            return FieldPrimitiveType.None;
        }

        public override FieldType GetType()
        {
            return FieldType.InverseCollection;
        }

        public override List<R> GetValue(T _this)
        {
            return _getter(_this);
        }

        public override void SetValue(T _this, List<R> value)
        {
            throw new Exception("Can not set value to inverse field");
        }
    }
}
