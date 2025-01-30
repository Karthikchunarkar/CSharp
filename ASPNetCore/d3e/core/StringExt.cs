using System.Text.RegularExpressions;

namespace d3e.core
{
    public static class StringExt
    {
        public static string FromCharCodes(IEnumerable<long> charCodes, long start, long end)
        {
            var codesList = charCodes.Skip((int)start).Take((int)(end - start)).Select(c => (char)c).ToArray();
            return new string(codesList);
        }

        public static string FromEnvironment(string name, string defaultValue)
        {
            var value = Environment.GetEnvironmentVariable(name);
            return value ?? defaultValue;
        }

        public static List<string> Split(string source, string regex)
        {
            return Regex.Split(source, regex).ToList();
        }

        public static string Substring(string source, long beginIndex, long endIndex)
        {
            return source.Substring((int)beginIndex, (int)(endIndex - beginIndex));
        }

        public static string FromCharCode(long charCode)
        {
            return char.ConvertFromUtf32((int)charCode);
        }

        public static long CompareTo(string on, string anotherString)
        {
            return string.Compare(on, anotherString, StringComparison.Ordinal);
        }

        public static string Get(string on, long index)
        {
            return on[(int)index].ToString();
        }

        public static long CodeUnitAt(string on, long index)
        {
            return on[(int)index];
        }

        public static long Length(string on)
        {
            return on.Length;
        }

        public static long GetHashCode(string on)
        {
            return on.GetHashCode();
        }

        public static bool StartsWith(string on, string pattern, long index)
        {
            return on.IndexOf(pattern, (int)index) == (int)index;
        }

        public static long LastIndexOf(string on, string what, long start)
        {
            return on.LastIndexOf(what, (int)start);
        }

        public static bool IsEmpty(string on)
        {
            return string.IsNullOrEmpty(on);
        }

        public static bool IsNotEmpty(string on)
        {
            return !string.IsNullOrEmpty(on);
        }

        public static string Plus(string on, object other)
        {
            return on + other;
        }

        public static string TrimLeft(string on)
        {
            return on.TrimStart();
        }

        public static string TrimRight(string on)
        {
            return on.TrimEnd();
        }

        public static string Times(string on, long times)
        {
            return string.Concat(Enumerable.Repeat(on, (int)times));
        }

        public static string PadLeft(string on, long width, string padding)
        {
            return on.PadLeft((int)width, padding.FirstOrDefault());
        }

        public static string PadRight(string on, long width, string padding)
        {
            return on.PadRight((int)width, padding.FirstOrDefault());
        }

        public static bool Contains(string on, string other, long startIndex)
        {
            return on.Substring((int)startIndex).Contains(other);
        }

        public static bool ContainsIgnoreCase(string on, string other, long startIndex)
        {
            return on.Substring((int)startIndex).IndexOf(other, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string ReplaceFirst(string on, string from, string to, long startIndex)
        {
            var fromStart = on.Substring((int)startIndex);
            var index = fromStart.IndexOf(from, StringComparison.Ordinal);
            if (index < 0) return on;
            return on.Substring(0, (int)startIndex) + fromStart.Remove(index, from.Length).Insert(index, to);
        }

        public static string ReplaceAll(string on, string from, string replace)
        {
            return on.Replace(from, replace);
        }

        public static string ReplaceRange(string on, long start, long end, string replacement)
        {
            return on.Substring(0, (int)start) + replacement + on.Substring((int)end);
        }

        public static List<long> GetCodeUnits(string on)
        {
            return on.Select(c => (long)c).ToList();
        }

        public static long IndexOf(string lookIn, string lookFor, long fromIndex)
        {
            return lookIn.IndexOf(lookFor, (int)fromIndex, StringComparison.Ordinal);
        }

        public static string Join(IEnumerable<object> what, string with)
        {
            return string.Join(with, what);
        }
    }

}
