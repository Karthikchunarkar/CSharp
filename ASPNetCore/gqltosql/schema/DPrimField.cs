
namespace gqltosql.schema
{
    public class DPrimField<T, R> : DField<T, R>
    {
        private Func<T, R> _getter;

        private Action<T, R> _setter;

        private FieldPrimitiveType _primType;

        private Func<T, R> _def;

        public DPrimField(DModel<T> decl, int index, string name, string column, FieldPrimitiveType primType, Func<T, R> getter,
        Action<T, R> setter) : this(decl, index, name, column, primType, getter, setter, null)
        {

        }

        public DPrimField(DModel<T> decl, int index, string name, string column, FieldPrimitiveType primType, Func<T, R> getter,
            Action<T, R> setter, Func<T, R> def) : base(decl, index, name, column)
        {

            this._primType = primType;
            this._getter = getter;
            this._setter = setter;
            this._def = def;
        }

        public override object FetchValue(T _this, IDataFetcher fetcher)
        {
            return fetcher.OnPrimitiveValue(GetValue(_this), this);
        }

        public override FieldPrimitiveType GetPrimitiveType()
        {
            return _primType;
        }

        public override FieldType GetType()
        {
            return FieldType.Primitive;
        }

        public override object GetValue(T _this)
        {
            return _getter(_this);
        }

        public override void SetValue(T _this, R value)
        {
            _setter(_this, value);
        }

        public R GetDefaultValue(T _this)
        {
            return _def(_this);
        }
    }
}
