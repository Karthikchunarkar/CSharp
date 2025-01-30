using gqltosql;
using static rest.ws.DataChangeTracker;

namespace rest.ws
{
    public class ObjectUsage
    {
        public Field field;
        public DisposableListener listener;
        public int parentType;
        public long parentId;
        public int fieldIdx;

        public ObjectUsage(int parentType, long parentId, int fieldIdx, Field field, DisposableListener listener)
        {
            this.parentType = parentType;
            this.parentId = parentId;
            this.fieldIdx = fieldIdx;
            this.field = field;
            this.listener = listener;
        }
        public override bool Equals(object obj)
        {
            if (obj is ObjectUsage)
            {
                ObjectUsage other = (ObjectUsage)obj;
                return object.Equals(other.listener, listener) && object.Equals(other.field, field);
            }
            return false;

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(field, listener);
        }
    }
}
