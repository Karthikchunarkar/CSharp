using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using security;

namespace security
{
    public class JwtTokenUtil
    {
        private readonly string _secret;
        private readonly long _validation;
        private readonly SymmetricSecurityKey _key;

        public JwtTokenUtil(string secret, long validation)
        {
            _secret = secret ?? throw new ArgumentNullException(nameof(secret));
            _validation = validation;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        }

        // Generate a JWT token
        public string GenerateToken(string subject, UserProxy userProxy)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, subject),
            new Claim("id", userProxy.UserId.ToString()),
            new Claim("type", userProxy.Type),
            new Claim("sessionId", userProxy.SessionId),
            new Claim("scopes", "ROLE_ADMIN") // Example scope
        };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(_validation),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Validate a JWT token
        public UserProxy ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // No tolerance for expiration time
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                // Extract claims
                var id = long.Parse(jwtToken.Claims.First(c => c.Type == "id").Value);
                var type = jwtToken.Claims.First(c => c.Type == "type").Value;
                var sessionId = jwtToken.Claims.First(c => c.Type == "sessionId").Value;

                return new UserProxy(type, id, sessionId);
            }
            catch
            {
                // Token validation failed
                return null;
            }
        }
    }
}
