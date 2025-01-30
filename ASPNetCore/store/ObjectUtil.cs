namespace store
{
    public class ObjectUtil
    {
        public static bool EqualsIgnoreCase(string object1, string object2)
        {
            if (ReferenceEquals(object1, object2))
            {
                return true;
            }
            if (object1 == null || object2 == null)
            {
                return false;
            }
            return string.Equals(object1, object2, StringComparison.OrdinalIgnoreCase);
        }
    }
}
