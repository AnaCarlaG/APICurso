using curso.api.Model.Usuarios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace curso.api.Configuration
{
    public class JwtService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(UsuarioViewModelOuput usuarioViewModelOuput)
        {
            var secret = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfiguration:Secret").Value);
            var symetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOuput.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOuput.Login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioViewModelOuput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwatSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwatSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwatSecurityTokenHandler.WriteToken(tokenGenerated);
            return token;
        }
    }
}
