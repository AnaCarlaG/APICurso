using curso.api.Business.Entities;
using curso.api.Business.Repositories.Interfaces;
using curso.api.Configuration;
using curso.api.Filter;
using curso.api.Infraestruture.Data;
using curso.api.Model;
using curso.api.Model.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _autenticationService;


        public UserController(IUserRepository userRepository, IAuthenticationService autenticationService)
        {
            _userRepository = userRepository;
            _autenticationService = autenticationService;

        }

        /// <summary>
        /// Método de login
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns>
        [SwaggerResponse(statusCode: 200, description: "sucesso ao autenticar o login", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "campos obrigatórios", Type = typeof(ValidaCampoViewModelOuput))]
        [SwaggerResponse(statusCode: 500, description: "erro de servidor", Type = typeof(ErroGenericoViewModel))]
        /// </returns>
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustom]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            User usuario = _userRepository.ObterUsuario(loginViewModelInput.Login);
            if(usuario == null)
            {
                return BadRequest("Houve um erro ao tentar acessar");
            }
            var usuarioViewModelOuput = new UsuarioViewModelOuput
            {
                Codigo = 1,
                Login = "Ana",
                Email = "ana@gamil.com"
            };

            var token = _autenticationService.GerarToken(usuarioViewModelOuput);

            return Ok(new { Token = token, Usuario = usuarioViewModelOuput });
        }

        /// <summary>
        /// Método de registrar um user e senha para logar
        /// </summary>
        /// <param name="registro">RegistrarViewModelInput registrarViewModelInput</param>
        /// <returns>
        [SwaggerResponse(statusCode: 200, description: "sucesso ao autenticar o login", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "campos obrigatórios", Type = typeof(ValidaCampoViewModelOuput))]
        [SwaggerResponse(statusCode: 500, description: "erro de servidor", Type = typeof(ErroGenericoViewModel))]
        /// </returns>
        [HttpPost]
        [Route("registrar")]
        public IActionResult Registrar(RegistrarViewModelInput registrarViewModelInput)
        {

            //var migrationPendente = context.Database.GetPendingMigrations();

            //if(migrationPendente.Count() > 0)
            //{
            //    context.Database.Migrate();
            //}

            var usuario = new User();
            usuario.Login = registrarViewModelInput.Login;
            usuario.Senha = registrarViewModelInput.Senha;
            usuario.Email = registrarViewModelInput.Email;

            _userRepository.Add(usuario);
            _userRepository.Commit();
            return Created("", registrarViewModelInput);
        }
    }
}
