using System.Numerics;

namespace gqltosql
{
    public class QueryTypeReader
    {
        private int _id;
        private int _type;
        private List<IValue> fields = new List<IValue>();

        public QueryTypeReader(int type)
        {
            this._type = type;
            this._id = -1;
        }

        public int Type {  get { return _type; } }
        public int Id 
        { 
            get { return _id; }
            set { _id = value; }
        }

        public void Read(object[] row, OutObject obj)
        {
            if(_id != -1)
            {
                if (row[_id] is BigInteger val)
                {
                    if(val == null)
                    {
                        return;
                    }
                }
                else if (row[_id] is string val2)
                {
                    obj.Add("id", row[_id]);
                }
            }
            obj.AddType(_type);
            foreach(var field in fields)
            {
                field.Read(row, obj);
            }
        }

        public void AddGeo(string field, int index)
        {
            fields.Add(new GeoValue(field, index));
        }

        public void Add(string field, int index)
        {
            if(field.Equals("id"))
            {
                _id = index;
                return;
            }
            fields.Add(new SimpleValue(field, index));
        }

        public void AddCustomField(int type, long id, String parentField, String valueField, String field, int index)
        {
            fields.Add(new CustomFieldValue(type, id, parentField, valueField, field, index));
        }

        public QueryReader AddRef(string field, int index)
        {
            RefValue rv = new RefValue(field, false, index);
            fields.Add(rv);
            return rv.Reader;
        }

        public QueryReader AddEmbedded(string field)
        {
            EmbeddedValue ev = new EmbeddedValue(field);
            fields.Add(ev);
            return ev.Reader;
        }

        public override string ToString()
        {
            return _type.ToString();
        }
    }
}
