using EasyTime.Application.Contract.IServices;
using EasyTime.Model.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyTime.Application.Generator
{
    public class TokenGenerator : ITokenGenerator
    {
        public async Task<string> GenerateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("UserId",user.Id.ToString()),
                new Claim("Email",user.Email),
                new Claim("UserName",user.UserName)
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Audience = "www.easytime.com",
                Issuer = "www.easytime.com",
                Expires = DateTime.Now.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes("I-Am-Amirhossein-Makineh-Secret-Key-2025")), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            ValidateToken(result);
            return result;
        }

        public  ClaimsPrincipal ValidateToken(string token)
        {
            var parameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("I-Am-Amirhossein-Makineh-Secret-Key-2025"))
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.CanReadToken(token);
            return  tokenHandler.ValidateToken(token, parameters, out SecurityToken securityToken);
        }
    }
}
