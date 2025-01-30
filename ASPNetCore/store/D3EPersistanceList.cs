namespace store
{
    public class D3EPersistanceList<E> : List<E>
    {
        private int _field;
        private DBObject _master;
        private bool _proxy;
        private bool _inverse;
        private bool _child;

        public D3EPersistanceList(DBObject master, int field) : this(master, field, false) { }

        public static D3EPersistanceList<E> Primitive(DBObject master, int field)
        {
            return new D3EPersistanceList<E>(master, field);
        }

        public static D3EPersistanceList<E> Reference(DBObject master, int field)
        {
            return new D3EPersistanceList<E>(master, field);
        }

        public static D3EPersistanceList<E> Child(DBObject master, int field)
        {
            var list = new D3EPersistanceList<E>(master, field);
            list._child = true;
            return list;
        }

        public static D3EPersistanceList<E> Inverse(DBObject master, int field)
        {
            var list = new D3EPersistanceList<E>(master, field);
            list._inverse = true;
            return list;
        }

        public D3EPersistanceList(DBObject master, int field, bool inverse)
        {
            _master = master;
            _field = field;
            _inverse = inverse;
        }

        public DBObject GetMaster()
        {
            return _master;
        }

        public int GetField()
        {
            return _field;
        }

        public void Unproxy(List<E> result)
        {
            _proxy = false;
            if (_child)
            {
                result.ForEach(item => UpdateMaster(item));
            }
            AddRange(result);
        }

        public void MarkProxy()
        {
            _proxy = true;
        }

        public void ClearProxy()
        {
            _proxy = false;
        }

        private void CheckProxy()
        {
            if (_proxy)
            {
                Database.Get().UnproxyCollection(this);
                _proxy = false;
            }
        }

        private void FieldChanged(List<E> ghost)
        {
            _master.CollFieldChanged(_field, this, ghost);
        }

        public new int Size
        {
            get
            {
                CheckProxy();
                return base.Count;
            }
        }

        public new bool IsEmpty
        {
            get
            {
                CheckProxy();
                return base.Count == 0;
            }
        }

        public new E this[int index]
        {
            get
            {
                CheckProxy();
                return base[index];
            }
            set
            {
                CheckProxy();
                var ghost = new List<E>(this);
                ghost[index] = value;
                FieldChanged(ghost);
                base[index] = value;
            }
        }

        public new void Add(E item)
        {
            CheckProxy();
            var ghost = new List<E>(this);
            ghost.Add(item);
            FieldChanged(ghost);
            if (_child)
            {
                UpdateMaster(item);
            }
            base.Add(item);
        }

        public new bool Remove(E item)
        {
            CheckProxy();
            var ghost = new List<E>(this);
            if (ghost.Remove(item))
            {
                FieldChanged(ghost);
            }
            return base.Remove(item);
        }

        public new void AddRange(IEnumerable<E> collection)
        {
            CheckProxy();
            var ghost = new List<E>(this);
            ghost.AddRange(collection);
            FieldChanged(ghost);
            if (_child)
            {
                foreach (var item in collection)
                {
                    UpdateMaster(item);
                }
            }
            base.AddRange(collection);
        }

        public new void InsertRange(int index, IEnumerable<E> collection)
        {
            CheckProxy();
            var ghost = new List<E>(this);
            ghost.InsertRange(index, collection);
            FieldChanged(ghost);
            if (_child)
            {
                foreach (var item in collection)
                {
                    UpdateMaster(item);
                }
            }
            base.InsertRange(index, collection);
        }

        public new int RemoveAll(Predicate<E> match)
        {
            CheckProxy();
            var ghost = new List<E>(this);
            int removedCount = ghost.RemoveAll(match); // Corrected call
            if (removedCount > 0)
            {
                FieldChanged(ghost);
            }
            return base.RemoveAll(match);
        }

        public new void Clear()
        {
            CheckProxy();
            if (Count > 0)
            {
                var ghost = new List<E>(this);
                ghost.Clear();
                FieldChanged(ghost);
                base.Clear();
            }
        }

        public new bool Contains(E item)
        {
            CheckProxy();
            return base.Contains(item);
        }

        public new void CopyTo(E[] array, int arrayIndex)
        {
            CheckProxy();
            base.CopyTo(array, arrayIndex);
        }

        public new IEnumerator<E> GetEnumerator()
        {
            CheckProxy();
            return base.GetEnumerator();
        }

        public new int IndexOf(E item)
        {
            CheckProxy();
            return base.IndexOf(item);
        }

        public new void Insert(int index, E item)
        {
            CheckProxy();
            var ghost = new List<E>(this);
            ghost.Insert(index, item);
            FieldChanged(ghost);
            base.Insert(index, item);
        }

        public new void RemoveAt(int index)
        {
            CheckProxy();
            var ghost = new List<E>(this);
            ghost.RemoveAt(index);
            FieldChanged(ghost);
            base.RemoveAt(index);
        }

        public new void ForEach(Action<E> action)
        {
            CheckProxy();
            base.ForEach(action);
        }

        public new void Sort(Comparison<E> comparison)
        {
            CheckProxy();
            var ghost = new List<E>(this);
            ghost.Sort(comparison);
            FieldChanged(ghost);
            base.Sort(comparison);
        }

        public new void Reverse()
        {
            CheckProxy();
            var ghost = new List<E>(this);
            ghost.Reverse();
            FieldChanged(ghost);
            base.Reverse();
        }

        public void SetAll(List<E> items)
        {
            CheckProxy();
            var ghost = new List<E>(this);
            ghost.Clear();
            ghost.AddRange(items);
            FieldChanged(ghost);
            base.Clear();
            base.AddRange(items);
            if (_child)
            {
                items.ForEach(item => UpdateMaster(item));
            }
        }

        private void UpdateMaster(object item)
        {
            var obj = (DBObject)item;
            obj.SetMasterObject(_master);
            obj.SetChildIdx(_field);
            obj.UpdateChanges();
        }

        public string Repo()
        {
            return _master.Repo();
        }

        public bool IsInverse()
        {
            return _inverse;
        }
    }
}
