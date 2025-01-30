using store;

namespace repository.jpa
{
    public interface D3ERepository <T> where T : DatabaseObject
    {
        public T FindById(long id);

        public T GetOne(long id);

        public List<T> FindAll();
    }
}
