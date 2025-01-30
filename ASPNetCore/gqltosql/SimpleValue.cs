namespace gqltosql
{
    public class SimpleValue : IValue
    {
        public string Field;
        private int _index;
        private bool _parent;

        public SimpleValue(string field, int index)
        {
            this.Field = field;
            this._index = index;
            this._parent = field.Equals("_parent");
        }

        public virtual object Read(object[] row, OutObject obj)
        {
            object val = row[_index];
            if (_parent)
            {
                obj.Parents.Add((long) val);
            }
            else
            {
                obj.Add(Field, val);
            }
            return val;
        }

        public override string ToString()
        {
            return Field;
        }
    }
}
