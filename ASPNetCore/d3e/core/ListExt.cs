using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace d3e.core
{
    public static class ListExt
    {
        public static List<T> Build<T>(Action<List<T>> builder)
        {
            var list = new List<T>();
            builder(list);
            return list;
        }

        public static List<T> Filled<T>(int length, T fill, bool growable)
        {
            var list = new List<T>(length);
            for (int i = 0; i < length; i++)
            {
                list.Add(fill);
            }
            return list;
        }

        public static void SetLength(long length)
        {
            // TODO
        }

        public static List<T> List<T>(long length)
        {
            if(length == 0)
            {
                return new List<T>();
            }

            return Filled(length, null, false);
        }

        public static List<T> From<T>(IEnumerable<T> elements, bool growable = false)
        {
            return elements.ToList();
        }

        public static List<T> AsList<T>(params T[] elements)
        {
            return elements.ToList();
        }

        public static List<T> Generate<T>(int length, Func<int, T> generator, bool growable = false)
        {
            var list = new List<T>(length);
            for (int i = 0; i < length; i++)
            {
                list.Add(generator(i));
            }
            return list;
        }

        public static List<T> Reversed<T>(List<T> source)
        {
            var reversedList = new List<T>(source);
            reversedList.Reverse();
            return reversedList;
        }

        public static void Sort<T>(List<T> source, Comparison<T> comparison)
        {
            source.Sort(comparison);
        }

        public static void Shuffle<T>(List<T> source, Random random)
        {
            for (int i = source.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                (source[i], source[j]) = (source[j], source[i]);
            }
        }

        public static int IndexOf<T>(List<T> source, T element, int start = 0)
        {
            return source.FindIndex(start, e => EqualityComparer<T>.Default.Equals(e, element));
        }

        public static void Insert<T>(List<T> source, int index, T element)
        {
            source.Insert(index, element);
        }

        public static void AddAll<T>(List<T> source, IEnumerable<T> elements)
        {
            source.AddRange(elements);
        }

        public static T RemoveAt<T>(List<T> source, int index)
        {
            var item = source[index];
            source.RemoveAt(index);
            return item;
        }

        public static List<T> SubList<T>(List<T> source, int start, int end)
        {
            return source.GetRange(start, end - start);
        }

        public static void RemoveWhere<T>(List<T> source, Predicate<T> match)
        {
            source.RemoveAll(match);
        }

        public static bool Every<T>(List<T> source, Func<T, bool> test)
        {
            return source.All(test);
        }

        public static bool Any<T>(List<T> source, Func<T, bool> test)
        {
            return source.Any(test);
        }

        public static T FirstWhere<T>(List<T> source, Func<T, bool> test, Func<T> orElse = null)
        {
            return source.FirstOrDefault(test) ?? (orElse?.Invoke() ?? default);
        }

        public static T LastWhere<T>(List<T> source, Func<T, bool> test, Func<T> orElse = null)
        {
            return source.LastOrDefault(test) ?? (orElse?.Invoke() ?? default);
        }

        public static void SetRange<T>(List<T> source, int start, int end, IEnumerable<T> replacement)
        {
            var replacementList = replacement.ToList();
            for (int i = start, j = 0; i < end && j < replacementList.Count; i++, j++)
            {
                source[i] = replacementList[j];
            }
        }

        public static Dictionary<int, T> AsMap<T>(List<T> source)
        {
            return source.Select((value, index) => new { value, index })
                         .ToDictionary(x => x.index, x => x.value);
        }

        public static bool IsNotEmpty<T>(List<T> source)
        {
            return source.Any();
        }

        public static T First<T>(List<T> source)
        {
            return source.First();
        }

        public static T Last<T>(List<T> source)
        {
            return source.Last();
        }

        public static List<T> Plus<T>(List<T> source, List<T> other)
        {
            return source.Concat(other).ToList();
        }
    }
}
