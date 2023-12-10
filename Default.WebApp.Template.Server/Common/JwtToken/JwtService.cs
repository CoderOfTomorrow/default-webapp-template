using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Default.WebApp.Template.Server.Common.JwtToken
{
    public class JwtService
    {
        public readonly TokenSettings tokenSettings;

        public JwtService(TokenSettings tokenSettings)
        {
            ArgumentNullException.ThrowIfNull(tokenSettings);

            this.tokenSettings = tokenSettings;
        }

        public string CreateAuthToken(string userId, string username, string[] roles)
        {
            List<Claim> claims =
            [
                new("userId", userId),
                new("username", username),
                .. roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList(),
            ];

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(tokenSettings.ExpirationInMinutes)),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}