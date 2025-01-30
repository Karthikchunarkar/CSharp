namespace rest.ws
{
    public class QueryResult
    {
        public string Type { get; set; } 
        public bool External { get; set; }
        public bool IsList { get; set; } 
        public object Value { get; set; }
        public IDisposable ChangeTracker { get; set; } 
    }
}
