namespace gqltosql.schema
{
    public class DRefField<T, R> : DField<T, R>
    {
        private Func<T, R> _getter;
        private Action<T, R> _setter;
        private bool _child;

        public DRefField(DModel<T> decl, int index, string name, string column, bool child, DModel<object> refernce, Func<T, R> getter,
            Action<T, R> setter) : base(decl, index, name, column)
        {
            this._child = child;
            _getter = getter;
            _setter = setter;
        }

        public override object FetchValue(T _this, IDataFetcher fetcher)
        {
            return fetcher.OnReferenceValue(GetValue(_this));
        }

        public override FieldPrimitiveType GetPrimitiveType()
        {
            return FieldPrimitiveType.None;
        }

        public override FieldType GetType()
        {
            return FieldType.Reference;
        }

        public override object GetValue(T _this)
        {
            return _getter(_this);
        }

        public override bool IsChild()
        {
            return _child;
        }

        public override void SetValue(T _this, R value)
        {
            _setter(_this, value);
        }
    }
}
