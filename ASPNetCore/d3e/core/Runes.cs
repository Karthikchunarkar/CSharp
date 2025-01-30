namespace d3e.core
{
    public class Runes
    {
        private IEnumerator<long> codePoints;

        public Runes(string s)
        {
            codePoints = GetStringCodePoints(s).GetEnumerator();
        }

        public IEnumerator<long> GetCodePoints()
        {
            return codePoints;
        }

        private IEnumerable<long> GetStringCodePoints(string s)
        {
            for (int i = 0; i < s.Length;)
            {
                int codePoint;
                if (i < s.Length - 1 && char.IsSurrogatePair(s[i], s[i + 1]))
                {
                    codePoint = char.ConvertToUtf32(s[i], s[i + 1]);
                    i += 2;
                }
                else
                {
                    codePoint = (int)s[i];
                    i++;
                }
                yield return codePoint;
            }
        }
    }
}
