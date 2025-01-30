using System.Text.RegularExpressions;
namespace d3e.core
{
    public class RegExp
    {
        private Regex _regex;
        private bool _caseSensitive;
        private bool _multiLine;

        public RegExp(string source, bool caseSensitive = false, bool multiLine = false)
        {
            _caseSensitive = caseSensitive;
            _multiLine = multiLine;

            // Configure Regex options based on parameters
            RegexOptions options = RegexOptions.None;
            if (!caseSensitive)
            {
                options |= RegexOptions.IgnoreCase;
            }
            if (multiLine)
            {
                options |= RegexOptions.Multiline;
            }

            _regex = new Regex(source, options);
        }

        public static string Escape(string text)
        {
            return Regex.Escape(text);
        }

        public Matcher FirstMatch(string input)
        {
            // TODO
            return null;
        }

        public IEnumerable<Matcher> AllMatches(string input)
        {
            return AllMatches(input, 0);
        }

        public IEnumerable<Matcher> AllMatches(string input, int start)
        {
            var matcher = _regex.Matches(input, start);
            foreach (Match match in matcher)
            {
                yield return new Matcher(match, input);
            }
        }

        public bool HasMatch(string input)
        {
            return _regex.IsMatch(input);
        }

        public string StringMatch(string input)
        {
            var match = _regex.Match(input);
            return match.Success ? match.Value : null;
        }

        public string Pattern()
        {
            return _regex.ToString();
        }

        public bool IsMultiLine()
        {
            return _multiLine;
        }

        public bool IsCaseSensitive()
        {
            return _caseSensitive;
        }
    }
}
