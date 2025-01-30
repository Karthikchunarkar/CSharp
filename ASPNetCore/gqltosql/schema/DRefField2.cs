using static System.Runtime.InteropServices.JavaScript.JSType;
using list;

namespace gqltosql.schema
{
    public class DRefField2<T, R> : DRefField<T, R>
    {
        private Func<T, TypeAndId> _reference;

        public DRefField2(DModel<T> decl, int index, string name, string column, bool child, DModel<object> reference,
            Func<T, R> getter, Action<T, R> setter, Func<T, TypeAndId> refGetter) : base(decl, index, name, column, child, reference, getter, setter)
        {
            this._reference = refGetter;
        }

        public new object GetValue(T _this)
        {
            object val = _reference(_this);
            return val != null ? val : base.GetValue(_this);
        }

    }
}
