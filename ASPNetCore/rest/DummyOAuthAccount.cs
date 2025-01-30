namespace rest
{
    public class DummyOAuthAccount : AbsOAuthAccount
    {
        public override string GetAuthorizationUri()
        {
            throw new NotImplementedException();
        }

        public override string GetClientId()
        {
            throw new NotImplementedException();
        }

        public override string GetClientSecret()
        {
            throw new NotImplementedException();
        }

        public override string GetScope()
        {
            throw new NotImplementedException();
        }

        public override string GetSuccessHtml()
        {
            throw new NotImplementedException();
        }

        public override string GetTokenUri()
        {
            throw new NotImplementedException();
        }

        public override void OnAccessToken(object context, string accessToken, string refreshToken, int expiresIn)
        {
            throw new NotImplementedException();
        }

        public override void OnAccessTokenFailed(object context)
        {
            throw new NotImplementedException();
        }

        protected override string Name()
        {
            return "DummyOAuth";
        }
    }
}
