using System.Collections.ObjectModel;
using d3e.core;

namespace store
{
    public interface EntityMutator
    {
        public void Save(DatabaseObject obj, bool local);
        public void Update(DatabaseObject obj, bool local);
        public void SaveOrUpdate(DatabaseObject obj, bool local);
        public void Finish();

        public void Clear();
        public bool Delete<T>(T obj, bool local) where T : DatabaseObject;
        public void PeformDeleteOrphan<T>(ICollection<T> oldList, Collection<T> newList) where T : DatabaseObject;
        public H GetHelperByInstance<T, H>(object fullType) where T : DatabaseObject where H : EntityHelper<T>;
        public void ProcessOnLoad(object entity, string repo);
        public void PreUpdate(DatabaseObject obj);
        public void PreDelete(DatabaseObject obj);
        public void MarkDirty(DatabaseObject obj, bool inverse);
        public bool IsInDelete(object obj, string repo);
        public void Unproxy(DatabaseObject obj);
        public void UnproxyCollection(D3EPersistanceList<object> list);
        public void UnproxyDFile(DFile file, string repo);
    }
}
