namespace rest
{
    public class JsonStream
    {
        private readonly TextWriter _writer;
        private bool _needComma;

        public JsonStream(TextWriter writer)
        {
            _writer = writer;
        }

        public void Put<T>(T obj, Action<T> action)
        {
            Append('{');
            _needComma = false;
            action(obj);
            Append('}');
            _needComma = true;
        }

        public void Put(string key, long value)
        {
            WriteComma();
            Quote(key);
            Append(':');
            Append(value.ToString());
            _needComma = true;
        }

        public void Put(string key, double value)
        {
            WriteComma();
            Quote(key);
            Append(':');
            Append(value.ToString());
            _needComma = true;
        }

        public void Put(string key, DateTime value)
        {
            WriteComma();
            Quote(key);
            Append(':');
            Append(value.ToString("O"));
            _needComma = true;
        }

        public void Put(string key, TimeSpan value)
        {
            WriteComma();
            Quote(key);
            Append(':');
            Append(value.ToString("c"));
            _needComma = true;
        }

        public void Put(string key, Enum value)
        {
            WriteComma();
            Quote(key);
            Append(':');
            if (value == null)
            {
                Append("null");
            }
            else
            {
                Quote(value.ToString());
            }
            _needComma = true;
        }

        private void Append(string str)
        {
            _writer.Write(str);
        }

        private void Append(char c)
        {
            _writer.Write(c);
        }

        private void Quote(string key)
        {
            Append('"');
            Append(key);
            Append('"');
        }

        private void Escape(string s)
        {
            foreach (char ch in s)
            {
                switch (ch)
                {
                    case '"':
                        Append("\\\"");
                        break;
                    case '\\':
                        Append("\\\\");
                        break;
                    case '\b':
                        Append("\\b");
                        break;
                    case '\f':
                        Append("\\f");
                        break;
                    case '\n':
                        Append("\\n");
                        break;
                    case '\r':
                        Append("\\r");
                        break;
                    case '\t':
                        Append("\\t");
                        break;
                    case '/':
                        Append("\\/");
                        break;
                    default:
                        if ((ch <= '\u001F') || (ch >= '\u007F' && ch <= '\u009F') || (ch >= '\u2000' && ch <= '\u20FF'))
                        {
                            Append($"\\u{(int)ch:X4}");
                        }
                        else
                        {
                            Append(ch);
                        }
                        break;
                }
            }
        }

        private void WriteComma()
        {
            if (_needComma)
            {
                Append(',');
            }
        }

        public void Put(string key, bool value)
        {
            WriteComma();
            Quote(key);
            Append(':');
            Append(value.ToString().ToLower());
            _needComma = true;
        }

        public void Put(string key, string value)
        {
            WriteComma();
            Quote(key);
            Append(':');
            if (value == null)
            {
                Append("null");
            }
            else
            {
                Append('"');
                Escape(value);
                Append('"');
            }
            _needComma = true;
        }

        public void Put<T>(string key, T obj, Action<T> action)
        {
            WriteComma();
            Quote(key);
            Append(':');
            if (obj == null)
            {
                Append("null");
            }
            else
            {
                Put(obj, action);
            }
            _needComma = true;
        }

        public void PutList(string key, IList<string> values)
        {
            WriteComma();
            Quote(key);
            Append(':');
            Append('[');
            for (int x = 0; x < values.Count; x++)
            {
                if (x != 0)
                {
                    Append(',');
                }
                Append('"');
                Escape(values[x]);
                Append('"');
            }
            Append(']');
            _needComma = true;
        }

        public void PutEnums<T>(string key, IList<T> values) where T : Enum
        {
            WriteComma();
            Quote(key);
            Append(':');
            Append('[');
            for (int x = 0; x < values.Count; x++)
            {
                if (x != 0)
                {
                    Append(',');
                }
                Append('"');
                Append(values[x].ToString());
                Append('"');
            }
            Append(']');
            _needComma = true;
        }

        public void PutObjs<T>(string key, IList<T> values, Action<T> action)
        {
            WriteComma();
            Quote(key);
            Append(':');
            Append('[');
            for (int x = 0; x < values.Count; x++)
            {
                if (x != 0)
                {
                    Append(',');
                }
                Put(values[x], action);
            }
            Append(']');
            _needComma = true;
        }

        public void PutObjSet<T>(string key, IList<T> values, Action<T> action)
        {
            PutObjs(key, new List<T>(values), action);
        }
    }
}
