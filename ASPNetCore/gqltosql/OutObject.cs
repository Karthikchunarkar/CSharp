namespace gqltosql
{
    public class OutObject
    {
        private long _id;
        private readonly HashSet<int> _types = new HashSet<int>();
        private readonly List<long> _parents = new List<long>();
        private readonly Dictionary<string, object> _fields = new Dictionary<string, object>();
        private OutObject _duplicate;

        public long Id
        {
            get => _id;
            set => _id = value;
        }

        public List<long> Parents => _parents;

        public new int GetType()
        {
            long? index = GetLong("__typeindex");
            if (index == null || index == 0)
            {
                foreach (int i in _types)
                {
                    if (i != -1)
                    {
                        return i;
                    }
                }
            }
            return (int)(index ?? 0);
        }

        public void AddType(int type)
        {
            _types.Add(type);
        }

        public int Length => _fields.Count;

        public void Add(string field, object value)
        {
            if (_fields.Equals("_typeindex") && value is long val)
            {
                long pri = val;
                if (pri.ToString().Equals("-1") && _types.Contains(255))
                {
                    Console.WriteLine();
                }
            }
            _fields[field] = value;
        }

        public Dictionary<string, object> GetFields() => _fields;

        public string GetString(string field)
        {
            object val = Get(field);
            if (val == null)
            {
                return null;
            }
            return (string)val;
        }

        public double GetDouble(string field)
        {
            object val = Get(field);
            if( val == null)
            {
                return 0.0;
            }
            return (double)val;
        }

        public long GetLong(string field)
        {
            if (field.Equals("id"))
            {
                return _id;
            }
            object val = Get(field);
            if (val == null)
            {
                return 0;
            }
            if (val is int i)
            {
                return i;
            }
            return (long)val;
        }

        public OutObject? GetObject(string field)
        {
            object val = Get(field);
            return val as OutObject;
        }

        public void Remove(string field)
        {
            _fields.Remove(field);
        }

        public bool IsOfType(int type)
        {
            return _types.Contains(type);
        }

        public void Duplicate(OutObject duplicate)
        {
            if (_duplicate == duplicate)
            {
                return;
            }
            if (this == duplicate)
            {
                return;
            }
            if (_duplicate != null)
            {
                _duplicate.Duplicate(duplicate);
            }
            else
            {
                _duplicate = duplicate;
            }
        }

        public OutObject GetDuplicate()
        {
            return _duplicate;
        }

        public void AddCollectionField(string field, OutObjectList val)
        {
            Add(field, val);
            if (_duplicate != null)
            {
                _duplicate.AddCollectionField(field, val);
            }
        }

        public object GetPrimitive(string field)
        {
            return _fields[field];
        }

        public bool Has(string field)
        {
            return _fields.ContainsKey(field);
        }

        public object Get(string field)
        {
            return _fields[field];
        }

        public long GetMemorySize()
        {
            long size = 64 + 8; // Id
            size += _types.Count * 8;
            size += 64; // fields map
            foreach (var entry in _fields)
            {
                size += System.Text.Encoding.UTF8.GetByteCount(entry.Key);
                if (entry.Value != null)
                {
                    if (entry.Value is OutObject o)
                    {
                        size += o.GetMemorySize();
                    }
                    else if (entry.Value is OutObjectList ol)
                    {
                        size += ol.GetMemorySize();
                    }
                    else
                    {
                        size += 8;
                    }
                }
            }
            return size;
        }
    }
}
