using ControleGastos.API.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControleGastos.API.Domain.Services.Classes
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario usuario)
        {
            // Manipula o token
            var tokenHandler = new JwtSecurityTokenHandler();

            // Como o token vai ser criado e especifica as informações dele
            byte[] key = Encoding.ASCII.GetBytes(_configuration["KeySecret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // As claims são as informações do usuario, como email, senha e identificador
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                }),

                // É necessário uma chave para assinar assinar o token, essa chave está no appsettings
                Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["HorasValidadeToken"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature
                ),    
                
            };


            // Token é gerado utilizando o handler e depois é escrito.
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

    }
}
