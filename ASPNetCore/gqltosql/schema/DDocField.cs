
namespace gqltosql.schema
{
    public class DDocField<T, R> : DRefField<T, R>
    {
        private string _idColumn;

        public DDocField(DModel<T> decl, int index, String name, String column, String idColumn, bool child, DModel<object> refernce,
            Func<T, R> getter, Action<T, R> setter) : base(decl, index, name, column, child, refernce, getter, setter) 
        {
            this._idColumn = idColumn;
        }

        public string IdColumn
        {
            get { return _idColumn; }
        }
    }
}
