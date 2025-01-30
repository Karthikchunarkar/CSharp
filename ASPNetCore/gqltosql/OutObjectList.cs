using System.Collections;

namespace gqltosql
{
    public class OutObjectList : List<OutObject>
    {
        private static long _serialVersionUID = 1L;

        public long GetMemorySize()
        {
            long size = 64;
            foreach (OutObject e in this)
            {
                size += e.GetMemorySize();
            }
            return size;
        }
    }
}
