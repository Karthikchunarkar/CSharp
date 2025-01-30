using java.lang;
using d3e.core;

namespace store
{
    public interface IEntityManager
    {
        public void Persist(DatabaseObject entity);
        public void Delete(DatabaseObject entity);
        public T Find<T>(int type, long id);
        public T GetById<T>(int type, long id);
        public List<T> FindAll<T>(int type);
        public void Unproxy(DatabaseObject obj);
        public void UnproxyCollection<T>(D3EPersistanceList<T> list);
        public void PersistFile(DFile file);
        public Query CreateNativeQuery(string sql);
        public void UnproxyDFile(DFile file);
        public void CreateId(DatabaseObject obj);
        public object GetCache();
        public List<T> GetByIds<T>(int type, List<long> ids);
        public List<T> LoadAll<T>(int type, long size, long page);
    }

}
