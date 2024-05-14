using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Test.Models;
using Test.Helper;
using System.Security.Claims;

namespace Test.Authentication
{
    public class LoginHandler
    {
        private readonly AppDbContext dbContext;
        private readonly JwtOptions jwt;

        public LoginHandler(AppDbContext dbContext, JwtOptions jwt)
        {
            this.dbContext = dbContext;
            this.jwt = jwt;
        }
        public (string token, User? user) CheckeMail(string email, string password)
        {
            
            var user = dbContext.Set<User>().SingleOrDefault(u => u.Email == email);

            // check if there is an email like that 
            if (user == null) 
                return ("email",null);

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                return ("password", null);

            var token = CreateToken(user);

            return (token, user);
            
        }

        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwt.Issuer,
                Audience = jwt.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey))
                ,SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.NameIdentifier , (user.Id).ToString()),
                    new (ClaimTypes.Name , user.Username)
                    
                })
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return accessToken;
        }
    }
}
