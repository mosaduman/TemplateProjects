using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Autofac.Core;
using BasicExtensions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OrderManagementSystem.Core;
using OrderManagementSystem.WebApi.Models;

namespace OrderManagementSystem.WebApi.Managers
{
    public class AuthenticationManager
    {
        private readonly IConfiguration _configuration;

        public AuthenticationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GetTokenAsync(UserLoginModel userModel)
        {
            try
            {
                var user = new User();

                if (user.Username != userModel.Username || user.Password != userModel.Password)
                    throw new AuthenticationException("Username or Password Fail.");

                var response = CreateToken(user);
                return response;
            }
            catch (AuthenticationException e)
            {
                throw new AuthenticationException(e.Message, e.InnerException);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
        public string CreateToken(User user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = Encoding.UTF8.GetBytes(_configuration.ReadSetting("SecretKey"));
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Hash, user.Id.ToString().CreateMD5()));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Username));
                claims.Add(new Claim(ClaimTypes.UserData, user.ToJson()));
                claims.Add(new Claim(ClaimTypes.Role, user.Role));
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(JsonConvert.DeserializeObject<int>(_configuration.ReadSetting("DefaultTokenLifeTimeInMinutes"))),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                        SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenValue = tokenHandler.WriteToken(token);

                return tokenValue;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}
