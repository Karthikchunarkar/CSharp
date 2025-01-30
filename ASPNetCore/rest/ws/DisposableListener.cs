using gqltosql;
using list;

namespace rest.ws
{
    public class DisposableListener : IDisposable
    {
        public bool disposed;
        public ClientSession session;
        private Field field;
        private TypeAndId obj;
        public List<TypeAndId> objects = new List<TypeAndId>();
        public HashSet<int> types = new HashSet<int>();

        public DisposableListener(Field field, TypeAndId obj, ClientSession session)
        {
            this.session = session;
            this.field = field;
            this.obj = obj;
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
