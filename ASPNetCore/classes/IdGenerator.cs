namespace classes
{
    public class IdGenerator
    {
        private static long id = 0;

        private static readonly object lockObject = new object();

        public static long GetNext()
        {
            lock (lockObject)
            {
                id++;
                return id;
            }
        }
    }
}
