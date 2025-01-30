using static System.Runtime.InteropServices.JavaScript.JSType;

namespace gqltosql.schema
{
    public class DFlatField<T, R> : DField<T, List<R>>
    {
        private Func<T, List<R>> _getter;

        private string[] _flatPaths;

        public DFlatField(DModel<T> decl, int index, string name, string column, string collTable, DModel<object> refernce,
            Func<T, List<R>> getter, params string[] flatPaths) : base(decl, index, name, column)
        {
            this._flatPaths = flatPaths;
            CollTable = CollTable;
            Reference = refernce;
            this._getter = getter;
        }

        public string[] FlatPaths
        { get { return _flatPaths; } }

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
            return FieldType.ReferenceCollection;
        }

        public override List<R> GetValue(T _this)
        {
            return _getter(_this);
        }

        public override void SetValue(T _this, List<R> value)
        {
            throw new Exception("Can not set value to flat field");
        }
    }
}
