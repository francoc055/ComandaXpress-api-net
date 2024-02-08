using comandaXpress_api_net.Models;
using comandaXpress_api_net.Services.IService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace comandaXpress_api_net.Services
{
    public class AutorizacionService : IAutorizacionService
    {

        readonly IConfiguration _configuration;

        public AutorizacionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarToken(Usuario usuario)
        {
            try
            {
                var key = _configuration.GetValue<string>("JwtSettings:key"); //accedo al valor del .json
                var keyBytes = Encoding.ASCII.GetBytes(key); //codifico la key en un array de bytes.

                var claims = new ClaimsIdentity(); //intancio un claim, para agregar la info.
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())); //el subject va a ser el id del usuario.
                claims.AddClaim(new Claim(ClaimTypes.Role, usuario.Rol));

                var credencialesToken = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature //tipo del algoritmo para encriptar el token
                    );



                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(9),
                    SigningCredentials = credencialesToken
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return tokenCreado;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            return null;
        }

    }
}
