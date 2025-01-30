using System.Collections;
using gqltosql.schema;

namespace gqltosql
{
    public class Field
    {
        private DField<object, object> _field;
        private List<Selection> _selections;

        public DField<object, object> FieldVar { get { return _field; } set { _field = value; } }

        public List<Selection> Selections { get { return _selections; } set { _selections = value; } }

        public Selection? GetSelectionForType(int type)
        {
            return _selections.FirstOrDefault(s => s.Type.GetIndex() == type);
        }
        public BitArray GetBitSet(List<int> types)
        {
            var set = new BitArray(0);
            foreach (var s in _selections)
            {
                if (types.Contains(s.Type.GetIndex()))
                {
                    set.Or(s.FieldSet);
                }
            }
            return set;
        }

        public Field Inspect2(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return this;
            }

            var subFields = path.Split('.');
            return Inspect2(this, 0, subFields);
        }

        protected static Field Inspect2(Field field, int i, string[] subFields)
        {
            if (i == subFields.Length)
            {
                return field;
            }

            foreach (var s in field.Selections)
            {
                foreach (var f in s.Fields)
                {
                    if (f._field.Name == subFields[i])
                    {
                        var res = Inspect2(f, i + 1, subFields);
                        if (res != null)
                        {
                            return res;
                        }
                    }
                }
            }

            return null;
        }
    }
}
