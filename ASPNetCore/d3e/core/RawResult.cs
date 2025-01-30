using System.Numerics;
using store;

namespace d3e.core
{
    public class RawResult
    {
        private CriteriaHelper _helper;

        private Object[] _row;

        private int i;

        public RawResult(CriteriaHelper helper, Object row)
        {
            this._row = row is Object[] val ? val : new Object[] { row };
            this._helper = helper;
        }

        public bool ReadBoolean()
        {
            return (bool)_row[i++];
        }

        public DateOnly? ReadDate()
        {
            Object val = this._row[i++];
            if (val == null)
            {
                return null;
            }
            if (val is DateOnly value)
            {
                return value;
            }
            else if (val is DateTime dateTime)
            {
                return DateOnly.FromDateTime(dateTime);
            }
            return DateOnly.FromDateTime((DateTime)val);
        }

        public DateTime? ReadDateTime()
        {
            Object val = this._row[i++];
            if (val == null)
            {
                return null;
            }
            if (val is DateTime dateTime)
            {
                return dateTime;
            }
            return (DateTime)val;
        }

        public double ReadDouble()
        {
            object val = this._row[i++];
            if (val == null)
            {
                return 0.0;
            }
            return (double)val;
        }

        public TimeSpan ReadDuration()
        {
            long millis = (long)_row[i++];
            return TimeSpan.FromMilliseconds(millis);
        }

        public string ReadString()
        {
            string str = (string)_row[i++];
            return str ?? string.Empty;
        }

        public long ReadInteger()
        {
            object o = _row[i++];
            if (o is BigInteger bigInt)
            {
                return (long) bigInt;
            }
            else if (o is long longVal)
            {
                return longVal;
            }
            return 0L;
        }

        public object ReadAny()
        {
            return this._row[i++];
        }

        public DatabaseObject ReadObject()
        {
            if (_row[i] == null)
            {
                i += 2;
                return null;
            }

            long id = (long)_row[i++];
            int refType = (int)_row[i++];
            var cache = (D3EPrimaryCache) _helper.Provider.Get().GetCache();
            return cache.GetOrCreate(refType, id);
        }

        public TimeOnly? ReadTime()
        {
            // TODO: Implement time reading logic
            return null;
        }

    }
}
