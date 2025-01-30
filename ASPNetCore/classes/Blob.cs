using System.Collections;

namespace classes
{
    public class Blob
    {

        private readonly byte[] arr;

        public Blob(byte[] arr)
        {   
            this.arr = arr;
        }

        public byte[] Bytes()
        {
            return arr;
        }

        public long CompareTo(Blob _old)
        {
            if(ReferenceEquals(_old, this))
            {
                return 0;
            }
            return StructuralComparisons.StructuralComparer.Compare(arr, _old.arr);
        }

        public long GetSize()
        {
            return arr.Length;
        }
    }
}
