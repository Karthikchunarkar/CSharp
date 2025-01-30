using models;
using rest;
using store;

namespace d3e.core
{
    public interface ExternalSystem
    {
        void Save(CreatableObject obj, bool local);

        void Delete(CreatableObject obj, bool local);

        T GetOne<T>(string type, long id);

        bool Unique(string checkIn, long checkInId, string checkFor, object value, string masterName, long masterId);

        T Singleton<T>(T env) where T : class;

        T Create<T>(GraphQLInputContext ctx) where T : class;

        T Delete<T>(string type, long gqlInputId) where T : class;

        T Update<T>(GraphQLInputContext ctx) where T : class;

        List<T> All<T>(Type context);

        void Unproxy(DatabaseObject obj);

        void UnproxyCollection(D3EPersistanceList<object> list);

        void UnproxyDFile(DFile file);

        H GetHelperByInstance<T, H>(object fullType)
        where T : DatabaseObject
        where H : EntityHelper<T>;

        H GetHelper<T, H>(string fullType)
            where T : DatabaseObject
            where H : EntityHelper<T>;

        IEntityManager GetEntityManager();

        void CreateId(DatabaseObject obj);

        bool TrackDirty();

    }
}
