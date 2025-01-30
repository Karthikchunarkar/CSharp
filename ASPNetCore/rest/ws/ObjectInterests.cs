namespace rest.ws
{
    public class ObjectInterests
    {
        public List<ObjectListener> fieldListeners = new List<ObjectListener>();
        public List<ObjectUsage> refListeners = new List<ObjectUsage>();

        public bool IsEmpty()
        {
            return true;
        }
    }
}
