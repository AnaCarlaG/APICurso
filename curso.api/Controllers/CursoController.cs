using curso.api.Business.Entities;
using curso.api.Business.Repositories.Interfaces;
using curso.api.Model;
using curso.api.Model.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        /// <summary>
        /// Registrar um curso
        /// </summary>
        /// <param name="cursoViewModel"></param>
        /// <returns>
        [SwaggerResponse(statusCode: 200, description: "sucesso ao autenticar o login", Type = typeof(CursoViewModel))]
        [SwaggerResponse(statusCode: 400, description: "campos obrigatórios", Type = typeof(ValidaCampoViewModelOuput))]
        [SwaggerResponse(statusCode: 500, description: "erro de servidor", Type = typeof(ErroGenericoViewModel))]
        /// </returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Registrar(CursoViewModel cursoViewModel)
        {
            Curso curso = new Curso();
            curso.Nome = cursoViewModel.Nome;
            curso.Descricao = cursoViewModel.Descricao;
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            curso.CodigoUsuario = codigoUsuario;

            _cursoRepository.Add(curso);
            _cursoRepository.Commit();
            return Created("", cursoViewModel);
        }

        /// <summary>
        /// Retorna uma lista de cursos
        /// </summary>
        /// <param name="registrarViewModelInput"></param>
        /// <returns>
        [SwaggerResponse(statusCode: 200, description: "sucesso ao autenticar o login", Type = typeof(CursoViewModel))]
        [SwaggerResponse(statusCode: 400, description: "campos obrigatórios", Type = typeof(ValidaCampoViewModelOuput))]
        [SwaggerResponse(statusCode: 500, description: "erro de servidor", Type = typeof(ErroGenericoViewModel))]
        /// </returns>
        [HttpGet]
        [Route("")]
        public IActionResult GetCursos()
        {

            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            _cursoRepository.ObterCursoPorUsuario(codigoUsuario).Select(s => new CursoViewModelOutput 
            {
                Nome = s.Nome,
                Descricao =s.Descricao,
                Login =s.Usuario.Login
            });

            //cursos.Add(new CursoViewModelOutput()
            //{
            //    Login = "",
           //     Descricao = "Teste",
           //     Nome = "teste"

          //  });
           // return Ok(cursos);
        }
    }
}
