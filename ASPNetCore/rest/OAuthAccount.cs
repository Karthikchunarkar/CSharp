namespace rest
{
    public interface OAuthAccount
    {
        public string CreateLink(object state);

        public string GetScope();

        public string GetClientId();

        public string GetAuthorizationUri();

        public string GetClientSecret();

        public string GetTokenUri();

        public string GetSuccessHtml();

        public void OnAccessToken(object context, string accessToken, string refreshToken, int expiresIn);

        public void OnAccessTokenFailed(object context);
    }
}
