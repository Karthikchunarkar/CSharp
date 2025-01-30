using store;

namespace d3e.core
{
    public class CloneContext
    {
        Dictionary<store.ICloneable, store.ICloneable> Cache = new Dictionary<store.ICloneable, store.ICloneable>();
        bool Reverting = false;
        bool CrearId = false;
        private bool IsOld = false;

        public CloneContext(bool crearId)
        {
            this.CrearId = crearId;
        }

        public CloneContext(bool crearId, bool isOld)
        {
            this.CrearId = crearId;
            this.IsOld = isOld;
        }

        public static CloneContext ForCloneAble(store.ICloneable obj, bool crearId)
        {
            return ForCloneAble(obj, crearId, false);
        }

        private static CloneContext ForCloneAble(store.ICloneable obj, bool crearId, bool isOld)
        {
            CloneContext cloneContext = new CloneContext(crearId, isOld);
            cloneContext.StartClone(obj);
            return cloneContext;
        }

        public T StartClone<T>(T obj) where T : store.ICloneable
        {
            var cloned = CreateNew(obj);
            Cache[obj] = cloned;
            obj.CollectChildValues(this);
            obj.DeepCloneIntoObj(cloned, this);

            if (CrearId && cloned is DatabaseObject dbObject)
            {
                dbObject.setId(0);
            }

            return (T)cloned;
        }

        public void CollectChilds<T>(List<T> exist) where T : store.ICloneable
        {
            foreach (var child in exist)
            {
                CollectChild(child);
            }
        }

        public void CollectChild<T>(T exist) where T : store.ICloneable
        {
            if (exist == null)
            {
                return;
            }
            T newObj = CreateNew(exist);
            Cache[exist] = newObj;
            exist.CollectChildValues(this);
        }

        private T? CreateNew<T>(T? exist) where T : store.ICloneable
        {
            if (exist == null)
            {
                return default;
            }
            T newObj = (T)exist.CreateNewInstance();
            if (newObj is DatabaseObject databaseObject)
            {
                var originalDbObject = exist as DatabaseObject;
                //databaseObject.setId(originalDbObject.Id); TODO Nikhil
                databaseObject.IsOld = true;
            }

            return newObj;
        }

        public void CloneChildList<T>(List<T> exist, Action<List<T>> setter) where T : store.ICloneable
        {
            List<T> array = new List<T>(exist);
            List<T> cloned = CloneRefList(array);
            setter.Invoke(cloned);
            for (int i = 0; i < array.Count; i++)
            {
                T c = cloned[i];
                array[i].DeepCloneIntoObj(c, this);
                if (CrearId && c is DatabaseObject dbObject)
                {
                    dbObject.setId(i);
                }
            }
        }

        public List<T> CloneRefList<T>(List<T> array) where T : store.ICloneable
        {
            List<T> cloned = new List<T>();
            foreach (var item in array)
            {
                cloned.Add((T) CloneRef(item));
            }

            return cloned;
        }

        public List<T> CloneRefSet<T>(HashSet<T> list) where T : store.ICloneable
        {
            List<T> cloned = new List<T>();
            foreach (var item in list)
            {
                cloned.Add((T)CloneRef(item));
            }

            return cloned;
        }

        public void CloneChild<T>(T exist, Action<T> setter) where T : store.ICloneable
        {
            if (exist == null)
            {
                setter.Invoke(default);
            }
            else
            {
                T cloned = (T)CloneRef(exist);
                setter.Invoke(cloned);
                exist.DeepCloneIntoObj(cloned, this);
                if (CrearId && cloned is DatabaseObject dbObject)
                {
                    dbObject.setId(0);
                }
            }
        }

        private T CloneRef<T>(T obj) where T : store.ICloneable
        {
            if(obj == null)
            {
                return default;
            }
            if(Reverting)
            {
                return (T) Cache[obj];
            }
            store.ICloneable exist;
            if(Cache.ContainsKey(obj))
            {
                exist = Cache[obj];
            } else
            {
                exist = obj;
            }
            return (T) exist;
        }

        public T GetFromCache<T>(T obj) where T : store.ICloneable
        {
            return (T) Cache.GetValueOrDefault(obj, null);
        }
    }
}
