using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAuthentication.Interfaces;
using UserAuthentication.Models.DTO;

namespace UserAuthentication.Services
{
    public class GenerateTokenService:IGenerateToken<UserDTO>
    {
        private readonly SymmetricSecurityKey _key;

        public GenerateTokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }
        public string GenerateToken(UserDTO userDTO)
        {
            string token = string.Empty;

            if (userDTO.Role != null)
            {
                //User identity
                var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, userDTO.UserId.ToString()),
                new Claim(ClaimTypes.Role, userDTO.Role)
            };



                //Signature algorithm
                var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

                //Assembling the token details
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(5),
                    SigningCredentials = cred
                };

                //Using the handler to generate the token
                var tokenHandler = new JwtSecurityTokenHandler();
                var myToken = tokenHandler.CreateToken(tokenDescription);
                token = tokenHandler.WriteToken(myToken);
                return token;
            }
            throw new Exception("UserDTO is null");
        }
    }
}
