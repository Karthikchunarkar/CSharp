using System.Collections;
using store;

namespace rest.ws
{
    public class TypeListener : IDisposable
    {
        public HashSet<int> types = new HashSet<int>();
        public bool disposed;
        public Action<DBObject, DBObject, StoreEventType> listener;
        public BitArray fields;
        public int type;

        public TypeListener(int type, BitArray fields, Action<DBObject, DBObject, StoreEventType> listener)
        {
            this.listener = listener;
            this.type = type;
            this.fields = fields;
        }
        public void Dispose()
        {
            disposed = true;
            Dispose();
        }
        public bool IsDisposed()
        {
            return disposed;
        }

    }
}
