using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GitHubSearchProject.DTOs;
using GitHubSearchProject.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace GitHubSearchProject.Services
{
    public class AuthService( IConfiguration config) : IAuthService
    {
        public async Task<LoginResponse> Login(LoginDTO loginDTO)
        {           
            if (loginDTO.UserName.ToLower() != "admin" || loginDTO.Password.ToLower() != "admin")
                return new LoginResponse(false, $"Invalid user credentials");

            string token = GenerateToken(loginDTO);
            return new LoginResponse(true, token);
        }
      
        private string GenerateToken(LoginDTO loginDTO)
        {
            //generate token
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(config.GetSection("JwtSettings:securitykey").Value!);
            var tokendesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, loginDTO.UserName),
                        new Claim(ClaimTypes.Role,"admin")
                }),
                Expires = DateTime.UtcNow.AddSeconds(3000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokendesc);
            var finaltoken = tokenhandler.WriteToken(token);
            return finaltoken;          
        }
    }
}
