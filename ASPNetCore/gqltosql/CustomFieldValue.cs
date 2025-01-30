namespace gqltosql
{
    public class CustomFieldValue : SimpleValue
    {
        private int _type;
        private long _id;
        private string _parentField;
        private string _valueField;


        public CustomFieldValue(int type, long id, string parentField, string valueField, string field, int index) : base(field, index)
        {
            this._type = type;
            this._id = id;
            this._parentField = parentField;
            this._valueField = valueField;
        }

        public override object Read(object[] row, OutObject obj)
        {
            OutObjectList list = (OutObjectList)obj.Get(_parentField);
            if (list == null)
            {
                list = new OutObjectList();
                obj.Add(_parentField, list);
            }

            var read = new OutObject();
            object val = base.Read(row, read);
            read.Id = _id;
            read.Add(_valueField, val);
            read.Remove(Field);
            read.AddType(_type);
            list.Add(read);

            return read;
        }
    }
}
