using BussinessObject.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository.Token
{
    public class ProvideToken
    {
        private readonly IConfiguration _configuration;
        private static ProvideToken _instance;
        public static ProvideToken Instance => _instance;
        private ProvideToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static void Initialize(IConfiguration configuration)
        {
            if (_instance == null)
                _instance = new ProvideToken(configuration);
        }

        public virtual string GenerateToken(BranchAccount user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["JWTSettings:SecretKey"];
            if (secretKey == null)
            {
                return "Not Found SecretKey";
            }
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.AccountId.ToString()),
                    new Claim("UserName", user.FullName),
                    //new Claim("FullName", user.FullName),
                    new Claim("Email", user.EmailAddress),
                    new Claim("Issuer", _configuration["JWTSettings:Issuer"]),
                    new Claim("Audience", _configuration["JWTSettings:Audience"]),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
