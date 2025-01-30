using Microsoft.EntityFrameworkCore;
using store;

namespace repository.jpa
{
    public abstract class AbstractD3ERepository<T> : D3ERepository<T> where T : DatabaseObject
    {
        private D3EEntityManagerProvider _provider;

        protected IEntityManager Em()
        {
            return this._provider.Get();
        }

        // Get the type index (to be implemented by derived classes)
        protected abstract int GetTypeIndex();

        // Find an entity by its ID
        public T FindById(long id)
        {
            return Em().GetById<T>(GetTypeIndex(), id);
        }

        public List<T> FingByIds(List<long> ids) { return Em().GetByIds<T>(GetTypeIndex(), ids); }

        public T GetOne(long id) { return Em().Find<T>(GetTypeIndex(), id); }

        public List<T> FindAll()
        {
            return Em().FindAll<T>(GetTypeIndex());
        }

        protected bool CheckUnique(Query query)
        {
            return (bool)query.GetSingleResult();
        }

        protected T GetXByY(Query query)
        {
            return query.GetObjectFirstResult<T>(GetTypeIndex());
        }

        protected List<T> GetAllXsByY(Query query)
        {
            return query.GetObjectResultList<T>(GetTypeIndex());
        }

        protected object GetOldValue(Query query)
        {
            return query.GetSingleResult();
        }


    }
}
