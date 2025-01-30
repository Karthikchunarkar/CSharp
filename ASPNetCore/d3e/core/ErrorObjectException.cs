namespace d3e.core
{
    public class ErrorObjectException : Exception
    {
        private object error;

        public object GetError()
        {
            return error;
        }

        public void SetError(object error)
        {
            this.error = error;
        }

        public ErrorObjectException(object error)
        {
            this.error = error;
        }

        private static readonly long SerialVersionUID = 1L;
    }
}
