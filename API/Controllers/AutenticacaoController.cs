using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Workflows;

namespace UStart.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/autenticacao")]    
    [AllowAnonymous] 
    public class AutenticacaoController : ControllerBase
    {
        private readonly UsuarioWorkflow _usuarioWorkFlow;
 
        public AutenticacaoController(UsuarioWorkflow usuarioWorkFlow)
        {
            _usuarioWorkFlow = usuarioWorkFlow;
        }

        [HttpPost]
        [Route("login")]        
        public IActionResult Login([FromBody] LoginCommand command)
        {            
            var loginToken = _usuarioWorkFlow.Login(command);
            if (loginToken == null)
            {
                return Unauthorized();
            }
            return Ok(loginToken);
        }


    }
}
