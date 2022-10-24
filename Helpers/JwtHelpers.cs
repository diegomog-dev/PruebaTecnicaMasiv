using Microsoft.IdentityModel.Tokens;
using PruebaTecnicaMasiv.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PruebaTecnicaMasiv.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.Username),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss"))
            };
            if(userAccounts.Username == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }else if(userAccounts.Username == "User")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User"));
            }
            return claims;
        }
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }
        public static UserTokens GenTokenKey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserTokens();
                if(model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigninKey);
                Guid Id;
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                userToken.Validity = expireTime.TimeOfDay;
                var jwToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256
                        )
                    );
                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.Username = model.Username;
                userToken.Id = model.Id;
                userToken.GuidId = Id;
                return userToken;
            }
            catch(Exception ex)
            {
                throw new Exception("Error generating the JWT", ex);
            }
        }
    }
}
