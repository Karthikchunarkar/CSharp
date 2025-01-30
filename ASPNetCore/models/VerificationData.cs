
namespace models
{
    public class VerificationData
    {
        public string Method { get; set; }
        public string Context { get; set; }
        public string Token { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Processed { get; set; }
        public VerificationData(string method, string context, string token, string subject, string body, bool processed)
        {
            Method = method;
            Context = context;
            Token = token;
            Subject = subject;
            Body = body;
            Processed = processed;
        }

        public VerificationData()
        {
            
        }
    }
}
