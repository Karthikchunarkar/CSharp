
using store;

namespace d3e.core
{
    public class TransactionManager
    {
        private static readonly AsyncLocal<TransactionManager> _threadLocal = new AsyncLocal<TransactionManager>();

        private readonly List<object> _added = new List<object>();
        private readonly List<object> _updated = new List<object>();
        private readonly List<object> _deleted = new List<object>();
        private bool _skipIds;

        public bool IsSkipIds => _skipIds;

        public void SkipIds()
        {
            _skipIds = true;
        }

        public void Add(object obj)
        {
            if (_added.Contains(obj) || _updated.Contains(obj))
            {
                return;
            }

            if (_deleted.Contains(obj))
            {
                _deleted.Remove(obj);
            }

            _added.Add(obj);
        }

        public void Update(object obj)
        {
            if (_added.Contains(obj) || _updated.Contains(obj) || _deleted.Contains(obj))
            {
                return;
            }

            _updated.Add(obj);
        }

        public void Delete(object obj)
        {
            if (_added.Contains(obj))
            {
                _added.Remove(obj);
                return;
            }

            if (_updated.Contains(obj))
            {
                _updated.Remove(obj);
                return;
            }

            _deleted.Add(obj);
        }

        public List<DataStoreEvent> GetChanges()
        {
            var list = new List<DataStoreEvent>();

            _added.ForEach(a => list.Add(new DataStoreEvent(StoreEventType.Insert, a)));
            _updated.ForEach(u => list.Add(new DataStoreEvent(StoreEventType.Update, u)));
            _deleted.ForEach(d => list.Add(new DataStoreEvent(StoreEventType.Delete, d)));

            return list;
        }

        public bool IsEmpty()
        {
            return _added.Count == 0 && _updated.Count == 0 && _deleted.Count == 0;
        }

        public bool GetIsEmpty()
        {
            return IsEmpty();
        }

        public static TransactionManager Get()
        {
            return _threadLocal.Value;
        }

        public static void Set(TransactionManager manager)
        {
            _threadLocal.Value = manager;
        }

        public static void Remove()
        {
            _threadLocal.Value = null;
        }

        public void ClearChanges()
        {
            foreach (Object obj in this._added)
            {
                if(obj is DBObject val)
                {
                    val._ClearChanges();
                }
                if(obj is DatabaseObject dbVal)
                {
                    dbVal.VisitChildern(a => a._clearChanges());
                }
            }
            foreach (Object obj in this._updated)
            {
                if (obj is DBObject val)
                {
                    val._ClearChanges();
                }
                if(obj is DatabaseObject dbVal)
                {
                    dbVal.VisitChildern(a => a._clearChanges());
                }
            }
        }
    }
}
