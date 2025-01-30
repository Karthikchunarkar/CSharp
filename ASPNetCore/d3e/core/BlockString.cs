namespace d3e.core
{
    public class BlockString
    {

        private string Content;

        private object Attachment { get => Attachment; set => Attachment = value; }

        public BlockString(string content)
        {
            this.Content = content;
        }

        public override string ToString()
        {
            return this.Content;
        }

    }
}
