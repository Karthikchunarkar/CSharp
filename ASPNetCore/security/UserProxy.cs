namespace security
{
    public class UserProxy
    {
        public string Type;
        public long UserId;
        public string SessionId;
        public string Token;
        public BaseUser User;

        public UserProxy(string type, long id, string sessionId)
        {
            this.Type = type;
            this.UserId = id;
            this.SessionId = sessionId;
        }
    }
}
