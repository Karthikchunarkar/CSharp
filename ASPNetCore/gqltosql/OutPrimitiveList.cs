using System.Collections;

namespace gqltosql
{
    public class OutPrimitiveList : List<OutObject>
    {
        private static readonly long _serialVersionUID = 1L;

        public long GetMemorySize()
        {
            long size = 64 + Count * 8;
            return size;
        }
    }
}
