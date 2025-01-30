using static System.Runtime.InteropServices.JavaScript.JSType;

namespace gqltosql.schema
{
    public class DEmbField<T, R> : DRefField<T, R>
    {
        private string _prefix;

        public DEmbField(DModel<T> decl, int index, string name, string column, string prefix, DModel<object> reference,
            Func<T, R> getter, Action<T, R> setter) : base(decl, index, name, column,true, reference, getter, setter)
        {
            this._prefix = prefix;
        }

        public string Prefix 
        {
            get { return _prefix; }
        }
    }
}
