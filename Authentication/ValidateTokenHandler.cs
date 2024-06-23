using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.Helper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Test.Authentication
{
    public class ValidateTokenHandler
    {
        private readonly JwtOptions jwt;

        public ValidateTokenHandler(JwtOptions jwt)
        {
            this.jwt = jwt;
        }
        public bool CheckToken(string token, out ClaimsPrincipal principal)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwt.SigningKey);

            // sets up the rules for what should be checked during validation
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwt.Issuer,
                ValidateAudience = true,
                ValidAudience = jwt.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };

            try
            {
                principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true;

            }
            catch (Exception ex)
            {
                principal = null;
                return false;
            }
        }

        public int GetUserId(string token)
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();

            if (CheckToken(token, out principal))
            {
                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
                return int.Parse(userIdClaim.Value);
            }
            else
                return -1;
        }

        public string GetUserName(string token)
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();

            if (CheckToken(token, out principal))
            {
                var usernameClaim = principal.FindFirst(ClaimTypes.Name);
                return usernameClaim.Value;
            }
            else
                return "The Token Not Valid";
        }
    }
}
