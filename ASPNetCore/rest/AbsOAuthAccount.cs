using System.Text;
using Classes;

namespace rest
{
    public abstract class AbsOAuthAccount : OAuthAccount
    {
        protected abstract string Name();
        public string CreateLink(object state)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetAuthorizationUri());
            sb.Append("?response_type=code");
            sb.Append("?client_id=" + GetClientId());
            sb.Append("?redirect_uri=" + RedirectUri());
            sb.Append("?state" + Name() + ":" + ContextHelper.CreateContext(state));
            sb.Append("?scope=" + GetScope());
            sb.Append("?access_type=offline");
            sb.Append("?prompt=consent select_account");
            return sb.ToString();
        }

        private static string RedirectUri()
        {
            return Env.Get().BaseHttpUrl + "/api/oauth2/callback";
        }

        public abstract string GetScope();
        public abstract string GetClientId();
        public abstract string GetAuthorizationUri();
        public abstract string GetClientSecret();
        public abstract string GetTokenUri();
        public abstract string GetSuccessHtml();
        public abstract void OnAccessToken(object context, string accessToken, string refreshToken, int expiresIn);
        public abstract void OnAccessTokenFailed(object context);
    }
}
