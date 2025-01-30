using System.Numerics;
using System.Runtime.Serialization;
using Classes;
using d3e.core;
using store;

namespace list
{
    public class NativeObj
    {
        private static DateTimeFormat _dtf = new DateTimeFormat("yyyy-MM-dd HH:mm:ss");

        private object[] _row;

        private long _id;

        public NativeObj(long id)
        {
            _row = new object[] { id };
            this._id = id;
        }

        public NativeObj(int size)
        {
            _row = new object[] { size };
        }

        public NativeObj(object row, int id)
        {
            if (!ClassUtils.GetType(row).IsArray)
            {
                row = new object[] { row };
            }
            this._row = (object[]?)row;
            SetId(id);
        }

        public void SetId(int id)
        {
            if (id != -1)
            {
                this._id = GetInteger(id);
            }
        }

        public string GetString(int index)
        {
            return (string)_row[index];
        }

        public long GetInteger(int id)
        {
            object o = this._row[id];
            if (o is BigInteger val)
            {
                return ((long)val);
            }
            else if (o is long val1)
            {
                return val1;
            }
            return 0L;
        }

        public double GetDouble(int index)
        {
            object o = this._row[index];
            if (o is double val)
            {
                return ((double)val);
            }
            return 0;
        }

        public bool GetBoolean(int index)
        {
            return (bool)_row[index];
        }

        public Geolocation GetGeolocation(int index)
        {
            string val = GetString(index);
            if (val == null)
            {
                return null;
            }
            string[] split = val.Split(',');
            Geolocation loc = new Geolocation(double.Parse(split[1]), double.Parse(split[0]));
            return loc;
        }

        public NativeObj GetRef(int index)
        {
            if (_row[index] == null)
            {
                return null;
            }
            return new NativeObj(_row[index], 0);
        }

        public override bool Equals(object? obj)
        {
            if (obj is NativeObj val)
            {
                long _id = val.Id;
                return _id == Id;
            }
            else if (obj is DatabaseObject val1)
            {
                long _id = val1.Id;
                return _id == Id;
            }
            return false;
        }

        public long Id { get { return _id; } }

        public object[] Row { get { return _row; } }

        public void Set(int index, object value)
        {
            _row[index] = value;
        }

        public List<NativeObj> GetListRef(int index)
        {
            string ids = (string)_row[index];
            List<NativeObj> allIds = ids.Substring(1, ids.Length - 2).Split(',')
                .Where(i => !i.Equals("NULL"))
                .Select(i => new NativeObj(long.Parse(i.Trim())))
                .ToList();
            return allIds;
        }

        public static List<NativeObj> GetListStruct(params List<object>[] props)
        {
            List<NativeObj> rows = new List<NativeObj>();
            int rowsSize = props[0].Count;
            for (int r = 0; r < rowsSize; r++)
            {
                object[] result = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    List<object> p = props[i];
                    object row = p[r];
                    result[i] = row;
                }
                rows.Add(new NativeObj(result, -1));
            }
            return rows;
        }

        public List<DateOnly> GetListDate(int index)
        {
            string ids = (string)_row[index];
            List<DateOnly> allIds = ids.Substring(1, ids.Length - 2).Split(',')
                .Where(i => !i.Equals("NULL"))
                .Select(i => DateOnly.Parse(i.Substring(1, i.Length - 2), _dtf.FormatProvider))
                .ToList();
            return allIds;
        }

        public DateOnly GetDate(int index)
        {
            object val = this._row[index];
            if (val == null)
            {
                return new DateOnly();
            }
            if (val is DateOnly val1)
            {
                return val1;
            }
            else if (val is DateTime val2)
            {
                return new DateOnly(val2.Year, val2.Month, val2.Day);
            }
            DateOnly value = (DateOnly)val;
            return new DateOnly(value.Year, value.Month, value.Day);
        }

        public int Size()
        {
            return _row.Length;
        }

        public List<double> GetListDouble(int index)
        {
            string ids = (string)_row[index];
            List<double> allIds = ids.Substring(1, ids.Length - 2).Split(',')
                .Where(i => !i.Equals("NULL"))
                .Select(i => double.Parse(i))
                .ToList();
            return allIds;
        }

        public List<string> GetListString(int index)
        {
            string ids = (string)_row[index];
            List<string> allIds = ids.Substring(1, ids.Length - 2).Split(',')
                .Where(i => !i.Equals("NULL"))
                .ToList();
            return allIds;
        }

        public DateTime GetDateTime(int index)
        {
            object val = this._row[index];
            if (val == null)
            {
                return new DateTime();
            }
            if (val is DateTime)
            {
                return (DateTime)val;
            }
            return (DateTime)val;
        }

        public List<DateTime> GetListDateTime(int index)
        {
            string ids = (string)_row[index];
            List<DateTime> allIds = ids.Substring(1, ids.Length - 2).Split(',')
                .Where(i => !i.Equals("NULL"))
                .Select(i => DateTime.Parse(i.Substring(1, i.Length - 2), _dtf.FormatProvider))
                .ToList();
            return allIds;
        }

        public long GetLong(int index)
        {
            object o = this._row[index];
            if (o is BigInteger val)
            {
                return (long)val;
            }
            else if (o is long)
            {
                return (long)o;
            }
            return 0L;
        }

        public List<bool> GetListBoolean(int index)
        {
            string ids = (string)_row[index];
            List<bool> allIds = ids.Substring(1, ids.Length - 2).Split(',')
                .Where(i => !i.Equals("NULL"))
                .Select(i => i.Equals("t"))
                .ToList();
            return allIds;
        }

        public List<int> GetListInteger(int index)
        {
            string ids = (string)_row[index];
            List<int> allIds = ids.Substring(1, ids.Length - 2).Split(',')
                .Where(i => !i.Equals("NULL"))
                .Select(i => int.Parse(i))
                .ToList();
            return allIds;
        }

        public T GetEnum<T>(int index) where T : Enum
        {
            if (_row[index] is Enum)
            {
                return (T)_row[index];
            }
            string name = (string)_row[index];
            if (name == null)
            {
                return default;
            }
            return GetEnum<T>(name);
        }

        private T GetEnum<T>(string name) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), name);
        }

        public List<T> GetListEnum<T>(int index) where T : Enum
        {
            string ids = (string)_row[index];
            List<T> allIds = ids.Substring(1, ids.Length - 2).Split(',')
                .Where(i => !i.Equals("NULL"))
                .Select(i => GetEnum<T>(i))
                .ToList();
            return allIds;
        }

        public IComparable GetComparable(int index)
        {
            if (_row[index] is Enum)
            {
                return ((Enum)_row[index]).ToString();
            }
            if (_row[index] is BigInteger)
            {
                return GetLong(index);
            }
            if (_row[index] is string)
            {
                return (string)_row[index];
            }
            if (_row[index] is bool)
            {
                return (bool)_row[index];
            }
            return null;
        }

        public TimeOnly GetTime(int index)
        {
            object val = this._row[index];
            if (val == null)
            {
                return new TimeOnly();
            }
            if (val is TimeOnly)
            {
                return (TimeOnly)val;
            }
            if (val is string)
            {
                return TimeOnly.Parse((string)val);
            }
            return new TimeOnly();
        }

        public List<TimeOnly> GetListTime(int index)
        {
            string ids = (string)_row[index];
            List<TimeOnly> allTimes = ids.Substring(1, ids.Length - 2).Split(',')
                .Where(i => !i.Equals("NULL"))
                .Select(i => TimeOnly.Parse(i.Substring(1, i.Length - 2), _dtf.FormatProvider))
                .ToList();
            return allTimes;
        }
    }
}
