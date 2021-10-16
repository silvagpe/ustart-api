using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UStart.Domain.Workflows;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Commands;

namespace UStart.API.Controllers
{
    /// <summary>
    /// Exemplo de controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/usuario")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioWorkflow _usuarioWorkflow;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(UsuarioWorkflow usuarioWorkflow, IUsuarioRepository usuarioRepository)
        {
            _usuarioWorkflow = usuarioWorkflow;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        [Route("autorizado")]
        public IActionResult UsuarioAutorizado()
        {
            return Ok("OK");
        }

        [HttpGet()]
        [Authorize]
        public IActionResult Pesquisar([FromQuery] string pesquisa)
        {
            var result = _usuarioRepository.Pesquisar(pesquisa != null ? pesquisa : "");
            return Ok(result);
            //return Ok(result == null? false : result.ToList());            
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _usuarioRepository.GetById(id);
            return Ok(result);
        }

        [HttpPost()]
        [Authorize]
        public IActionResult Add([FromBody] UsuarioCommand command)
        {
            _usuarioWorkflow.Add(command);
            if (_usuarioWorkflow.IsValid())
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(_usuarioWorkflow.GetErrors());
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UsuarioCommand command)
        {
            _usuarioWorkflow.Update(id, command);
            if (_usuarioWorkflow.IsValid())
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(_usuarioWorkflow.GetErrors());
            }
        }

    }
}
