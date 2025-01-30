namespace store
{
    public class DataStoreEvent
    {
        private StoreEventType _type;

        private object _entity;

        public DataStoreEvent(StoreEventType type, object entity)
        {
            this._type = type;

            this._entity = entity;
        }

        public new StoreEventType GetType()
        {
            return _type;
        }

        public object GetEntity()
        {
            return _entity;
        }

        public void SetEntity(object entity)
        {
            _entity = entity;
        }

        public void SetType(StoreEventType type)
        {
            _type = type;
        }
    }
}
