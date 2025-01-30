namespace gqltosql
{
    public class AliasGenerator
    {
        private static char[] _chars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
            'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        private static HashSet<string> _sql_key_words = new HashSet<string> {
            "as", "on"
        };

        private int _i;

        private string _prefix = "";

        private AliasGenerator _pre;

        private Dictionary<string, string> _history = new Dictionary<string, string>();

        public AliasGenerator()
        {
            
        }

        public string Next()
        {
            if(_i == _chars.Length)
            {
                _i = 0;
                if(_pre == null)
                {
                    _pre = new AliasGenerator();
                }
                _prefix = _pre.Next();
            }
            string n = _prefix + _chars[_i++];
            if(_sql_key_words.Contains(n))
            {
                return Next();
            } 
            else
            {
                return n;
            }
        }

        public void Store(string tableName, string forColumn, string alias)
        {
            _history[tableName + "-" + forColumn] = alias;
        }

        public string GetAlias(string tableName, string forColumn)
        {
            return _history[tableName + "-" + forColumn];
        }

    }
}
