using System.Text.RegularExpressions;

namespace d3e.core
{
    public class Matcher
    {
        private Match _matcher;

        private string _input;

        public Matcher(Match matcher, string input)
        {
            this._matcher = matcher;
            this._input = input;
        }

        public long GetStart()
        {
            return _matcher.Index;
        }

        public long GetEnd()
        {
            return _matcher.Index + _matcher.Length;
        }

        public string Group(long group)
        {
            return _matcher.Groups[(int)group]?.Value ?? string.Empty;
        }

        public string Get(long group)
        {
            return _matcher.Groups[(int)group]?.Value ?? string.Empty;
        }

        public List<string> Groups(List<long> groups)
        {
            return groups.Select(index => Group(index)).ToList();
        }

        public int GetGroupCount()
        {
            return _matcher.Groups.Count + 1;
        }

        public string GetInput()
        {
            return _input;
        }

        public Match GetPattern()
        {
            return _matcher.NextMatch();
        }
    }
}
