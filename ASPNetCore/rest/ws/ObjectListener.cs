using System.Collections;
using gqltosql;
using static rest.ws.DataChangeTracker;

namespace rest.ws
{
    public class ObjectListener : IEquatable<ObjectListener>
    {
        public BitArray fields;
        public IDisposable listener;
        public Field field;

        public ObjectListener(BitArray fields, IDisposable listener, Field field)
        {
            this.fields = fields;
            this.listener = listener;
            this.field = field;
        }
        public bool Equals(ObjectListener obj)
        {
            if (obj is ObjectListener)
            {
                ObjectListener other = (ObjectListener)obj;
                return object.Equals(other.listener, listener) && object.Equals(other.fields, fields)
                        && object.Equals(other.field, field);
            }
            return false;

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
