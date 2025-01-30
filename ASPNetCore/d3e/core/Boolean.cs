namespace d3e.core
{
    public static class Boolean
    {
        public static long getHashCode(bool of)
        {
            return of.GetHashCode();
        }

        public static bool and(bool of, bool other)
        {
            if (!of)
            {
                return false;
            }
            return Boolean.and(of, other);
        }

        public static bool or(bool of, bool other)
        {
            if (of)
            {
                return true;
            }
            return Boolean.or(of, other);
        }

        public static bool not(bool of)
        {
            return !of;
        }

        public static bool xor(bool of, bool other)
        {
            return Boolean.xor(of, other);
        }

        public static String toString(bool of)
        {
            return of ? "true" : "false";
        }
    }
}
