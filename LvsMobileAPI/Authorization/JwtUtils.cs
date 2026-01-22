using Common.ApiUri;
using Common.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LvsMobileAPI.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateToken(Users user);
        public int? ValidateToken(string token);
    }

    public class JwtUtils : IJwtUtils
    {

        public int? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = GetJwtKey();
            var jwtKeyByte = Encoding.ASCII.GetBytes(jwtKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKeyByte),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public string GenerateToken(Users logUser)
        {
            // claims
            var claim = new[]
             {
                                    new Claim(ClaimTypes.NameIdentifier, logUser.Id.ToString()),
                                    new Claim(ClaimTypes.GivenName, logUser.LoginName),
                                    new Claim(ClaimTypes.Name, logUser.Name),
                                };


            //// configuration from appsettings.development.json 
            //var configuration = new ConfigurationBuilder()
            //                    .SetBasePath(Directory.GetCurrentDirectory())
            //                    .AddJsonFile($"appsettings.Development.json");

            ////--- key from appsettings.development.json
            //var config = configuration.Build();
            //string jwtKey = config.GetSection("Jwt").GetValue<string>("Key");
            string jwtKey = GetJwtKey();
            // create token
            var token = new JwtSecurityToken
                (
                    issuer: apiServerUri.GetApiServerUri(),
                    audience: apiServerUri.GetApiServerUri(),
                    claims: claim,
                    expires: DateTime.UtcNow.AddHours(1),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(
                                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                                                SecurityAlgorithms.HmacSha256
                                            )
                );
            // write token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private string GetJwtKey()
        {
            // configuration from appsettings.development.json 
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile($"appsettings.Development.json");

            //--- key from appsettings.development.json
            var config = configuration.Build();
            string jwtKey = config.GetSection("Jwt").GetValue<string>("Key");
            return jwtKey;
        }
    }
}
