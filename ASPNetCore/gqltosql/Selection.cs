using System.Collections;
using sun.misc;
using gqltosql.schema;

namespace gqltosql
{
    public class Selection
    {
        private DModel<object> _type;
        private List<Field> _fields;

        public Selection(DModel<object> type, List<Field> fields)
        {
            this._type = type;
            this._fields = fields;
        }
        private BitArray _fieldsSet;

        public DModel<object> Type
        {
            get { return _type; }
        }

        public List<Field>  Fields { get { return _fields; } }

        public BitArray FieldSet
        {
            get
            {
                if (_fieldsSet == null)
                {
                    _fieldsSet = new BitArray(_type.GetFieldsCount());
                    foreach (var f in _fields)
                    {
                        _fieldsSet.Set(f.FieldVar.Index, true);
                    }
                }
                return _fieldsSet;
            }
        }
    }
}
