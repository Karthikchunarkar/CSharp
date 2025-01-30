namespace d3e.core
{
    public class FormatException : Exception
    {
        private static readonly long SerialVersionUID = 1L;
        private String Message;
        private String Source;
        private long Offset;

        public FormatException(Exception e) : base(e.Message, e)
        {
            
        }

        public FormatException(String message, String source, long offset)
        {
            this.Message = message;
            this.Source = source;
            this.Offset = offset;
        }
    }
}
