namespace gqltosql.schema
{
    public class DDocCollField<T, R> : DRefCollField<T, R>
    {
        private string _docColumn;
        public DDocCollField(DModel<T> decl, int index, string name, string column, string docColumn, bool child, string collTable, DModel<object> refernce, Func<T, List<R>> getter, Action<T, List<R>> setter) : base(decl, index, name, column, child, collTable, refernce, getter, setter)
        {
            this._docColumn = docColumn;
        }

        public string DocColumn
        {
            get { return _docColumn; }
        }
    }
}
