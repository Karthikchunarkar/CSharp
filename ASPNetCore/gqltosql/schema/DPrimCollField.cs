using static System.Runtime.InteropServices.JavaScript.JSType;

namespace gqltosql.schema
{
    public class DPrimCollField<T, R> : DField<T, List<R>>
    {
        private Func<T, List<R>> _getter;

        private Action<T, List<R>> _setter;

        private FieldPrimitiveType _primType;

        public DPrimCollField(DModel<T> decl, int index, string name, string column, string collTable,
            FieldPrimitiveType primType, Func<T, List<R>> getter, Action<T, List<R>> setter) : base(decl, index, name, column)
        {
            CollTable = collTable;
            _primType = primType;
            _getter = getter;
            _setter = setter;
        }

        public override object FetchValue(T _this, IDataFetcher fetcher)
        {
            return fetcher.OnPrimitiveList(GetValue(_this), this);
        }

        public override FieldPrimitiveType GetPrimitiveType()
        {
           return _primType;
        }

        public override FieldType GetType()
        {
            return FieldType.PrimitiveCollection;
        }

        public override List<R> GetValue(T _this)
        {
            return _getter(_this);
        }

        public override void SetValue(T _this, List<R> value)
        {
            _setter(_this, value);
        }
    }
}
