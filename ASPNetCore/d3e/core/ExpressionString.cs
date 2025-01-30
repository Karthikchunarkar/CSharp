namespace d3e.core
{
    public class ExpressionString
    {
        private string _content { get => _content; set => _content = value; }

        private object _attachment { get => _attachment; set => _attachment = value; }

        public ExpressionString()
        {

        }

        public ExpressionString(string content)
        {
            this._content = content;
        }

        public override string ToString()
        {
            return this._content;
        }

    }
}
