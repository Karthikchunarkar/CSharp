using Classes;

namespace rest.ws
{
    public interface Authenticator
    {
        public LoginResult login(string email, string phone, string username, string password, string deviceToken,
            string token, string code, string clientAddress);
    }
}
