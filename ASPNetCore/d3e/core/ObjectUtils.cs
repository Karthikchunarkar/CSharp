using System.Security.Cryptography.Xml;
using Classes;
using store;

namespace d3e.core
{
    public class ObjectUtils
    {
        static bool IsEquals(object a, object b)
        {
            if (a == b) return true;
            if (a == null || b == null) return false;
            if (ClassUtils.GetType(a) == ClassUtils.GetType(b) && a is DatabaseObject val)
            {
                return val.Id == val.Id;
            }
            return false;
        }

        static bool IsNotEquals(object a, object b)
        {
            return !IsEquals(a, b);
        }

        public static int Compare<T>(T a, T b)
        {
            if (a == null || b == null)
            {
                if (a == null && b == null)
                {
                    return 0;
                }
            }
            if (a == null)
            {
                return -1;
            }

            if (a.GetType() != b.GetType())

            {
                throw new Exception("Cannot compare two different types");
            }

            if (a is IComparable val)
            {
                IComparable one = (IComparable)a;
                IComparable two = (IComparable)b;

                return one.CompareTo(two);
            }

            string aStr = a.ToString();
            string bStr = b.ToString();
            return String.Compare(aStr, bStr, StringComparison.Ordinal);
        }

        public static bool IsLessThan<T>(T a, T b)
        {
            return Compare(a, b) < 0;
        }

        public static bool IsGrethanThan<T>(T a, T b)
        {
            return Compare<T>(a, b) > 0;
        }
    }


}
