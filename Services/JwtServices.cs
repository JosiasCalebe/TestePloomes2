using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TesteAPI.Models;

namespace TesteAPI.Services
{
    public class JwtServices
    {
        private readonly JwtConfig jwtConfig;

        public JwtServices(JwtConfig jwtConfig)
        {
            this.jwtConfig = jwtConfig;
        }

        public int TempoEmHoras()
        {
            return jwtConfig.TimeInHours;
        }

        public string GerarToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("id", user.UsuarioId.ToString()),
                    new Claim("email", user.Email)
                }),

                Expires = DateTime.Now.AddHours(jwtConfig.TimeInHours),

                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenJwt = tokenHandler.WriteToken(token);

            return tokenJwt;
        }

        // método para validar o token recebido
        public bool ValidarToken(string token)
        {
            bool isValid = false;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

                var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromHours(jwtConfig.TimeInHours)
                }, out SecurityToken validatedToken);

               
                return isValid = claims != null ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
