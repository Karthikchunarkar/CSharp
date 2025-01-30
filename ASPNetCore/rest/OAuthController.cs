using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Classes;

namespace rest
{
    [ApiController]
    [Route("api/oauth2")]
    public class OAuthController : ControllerBase
    {
        private readonly IDictionary<string, OAuthAccount> _accounts;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public OAuthController(
            IDictionary<string, OAuthAccount> accounts,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _accounts = accounts;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private string RedirectUri()
        {
            return $"{_configuration["BaseHttpUrl"]}/api/oauth2/callback";
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state)
        {
            var name = state.Split(':')[0];
            if (!_accounts.TryGetValue(name, out var account))
            {
                return BadRequest("Invalid account");
            }

            var requestBody = new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["code"] = code,
                ["redirect_uri"] = RedirectUri(),
                ["client_id"] = account.GetClientId(),
                ["client_secret"] = account.GetClientSecret()
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(account.GetTokenUri(), content);
            var context = ContextHelper.ExtractContext(state.Replace($"{name}:", ""));

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(responseBody);

                var accessToken = jsonResponse["access_token"].ToString();
                var refreshToken = jsonResponse["refresh_token"].ToString();
                var expiresIn = jsonResponse["expires_in"].Value<int>();

                account.OnAccessToken(context, accessToken, refreshToken, expiresIn);
                return Content(account.GetSuccessHtml(), "text/html");
            }

            account.OnAccessTokenFailed(context);
            return BadRequest("Error exchanging code for token");
        }
    }
}
