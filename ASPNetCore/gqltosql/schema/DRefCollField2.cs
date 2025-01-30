
using System.Collections;
using list;

namespace gqltosql.schema
{
    public class DRefCollField2<T, R> : DRefCollField<T, R>
    {
        private Func<T, List<TypeAndId>> _refGetter;

        public DRefCollField2(DModel<T> decl, int index, string name, string column, bool child, string collTable,
            DModel<object> refernce, Func<T, List<R>> getter, Action<T, List<R>> setter,
            Func<T, List<TypeAndId>> refGetter) : base(decl, index, name, column, child, collTable, refernce, getter, setter)
        {
            this._refGetter = refGetter;
        }

        public IList getValue(T _this)
        {
            IList val = _refGetter(_this);
            return val != null ? val : base.GetValue(_this);
        }
    }
}
