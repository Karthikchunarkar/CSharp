using d3e.core;

namespace gqltosql
{
    public class GeoValue : IValue
    {
        private string _field;

        private int _index;

        public GeoValue(string field, int index)
        {
            this._field = field;
            this._index = index;
        }

        public object Read(object[] row, OutObject obj)
        {
            object val = row[_index];
            Geolocation loc;
            if (val == null)
            {
                loc = null;
            } 
            else
            {
                string[] split = val.ToString().Split(",");
                loc = new Geolocation(double.Parse(split[1]), double.Parse(split[0]));
            }
            obj.Add(_field, loc);
            return loc;
        }

        public override string ToString()
        { 
            return _field;
        }
    }
}
