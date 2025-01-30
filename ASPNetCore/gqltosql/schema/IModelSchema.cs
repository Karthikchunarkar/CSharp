using store;

namespace gqltosql.schema
{
    public interface IModelSchema
    {
        public List<DModel<object>> GetAllTypes();

        public DModel<object> GetType(string type);

        public DModel<object> GetType(int index);

        public DModel<object> Get(DBObject obj);

        public List<DClazz> GetAllChannels();

        public DClazz GetChannel(string name);

        public List<DClazz> GetAllRPCs();

        public DClazz GetRPC(string name);

        public long GetDatabaseId(DatabaseObject obj);

        public DField<object, object> GetCollectionField(DModel<object> type, D3EPersistanceList<object> list);

        public long CreateCompoundId(int type, long id);

        public long ExtractCompoundId(long id);

        public DatabaseObject CreateNewInstance(int type, long id);

        public void AssignObjectId(DModel<object> type, DatabaseObject obj, long id);

        public DBChange FindChanges(DBObject _this);

        public void MarkInProxy(DatabaseObject obj, bool inProxy);
    }
}
