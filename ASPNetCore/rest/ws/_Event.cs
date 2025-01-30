using gqltosql;
using security;
using store;

namespace rest.ws
{
    public class _Event
    {
        public _EventType type;
        public TypeListener tl;
        public DisposableListener dl;
        public object obj;
        public Field field;
        public DBObject obj2;
        public DBObject old;
        public StoreEventType changeType;
        public UserProxy userProxy;
        public D3EPrimaryCache cache;
        public Dictionary<DBObject, DBChange> changes;
    }
}
