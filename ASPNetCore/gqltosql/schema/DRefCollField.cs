using static System.Runtime.InteropServices.JavaScript.JSType;

namespace gqltosql.schema
{
    public class DRefCollField<T, R> : DField<T, List<R>>
    {

        private Func<T, List<R>> _getter;

        private Action<T, List<R>> _setter;

        private bool _child;

        private bool _customFields;

        public DRefCollField(DModel<T> decl, int index, string name, string column, bool child, string collTable,
            DModel<object> refernece, Func<T, List<R>> getter, Action<T, List<R>> setter) : base(decl, index, name, column)
        {
            CollTable = collTable;
            Reference = refernece;
            _getter = getter;
            _setter = setter;
        }

        public bool CustomFields { get { return _customFields; } }

        public override bool IsChild()
        {
            return _child;
        }

        public override object FetchValue(T _this, IDataFetcher fetcher)
        {
            return fetcher.OnReferenceList(GetValue(_this));
        }

        public override FieldPrimitiveType GetPrimitiveType()
        {
            return FieldPrimitiveType.None;
        }

        public override FieldType GetType()
        {
            return FieldType.ReferenceCollection;
        }

        public override List<R> GetValue(T _this)
        {
            return _getter(_this);
        }

        public override void SetValue(T _this, List<R> value)
        {
            _setter(_this, value);
        }

        public DRefCollField<T, R> MarkCustomFields()
        {
            this._customFields = true;
            return this;
        }
    }
}
