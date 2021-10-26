using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Workflows;

namespace UStart.API.Controllers
{

    /// <summary>
    /// Exemplo de rota http://localhost:5000/api/v1/cliente/id
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cliente")]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository clienteRepository;
        private readonly ClienteWorkflow clienteWorkflow;

        public ClienteController(IClienteRepository clienteRepository, ClienteWorkflow clienteWorkflow)
        {
            this.clienteRepository = clienteRepository;
            this.clienteWorkflow = clienteWorkflow;
        }
        
        [HttpGet]        
        public IActionResult Get([FromQuery] string pesquisa)
        {
            return Ok(clienteRepository.Pesquisar(pesquisa));
        }
        
        [HttpGet]    
        [Route("{id}")]    
        public IActionResult GetPorId([FromRoute] Guid id)
        {
            return Ok(clienteRepository.ConsultarPorId(id));
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] ClienteCommand command )
        {
            clienteWorkflow.Add(command);
            if (clienteWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(clienteWorkflow.GetErrors());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Atualizar([FromRoute] Guid id, [FromBody] ClienteCommand command )
        {
            clienteWorkflow.Update(id, command);
            if (clienteWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(clienteWorkflow.GetErrors());
        }

        [HttpDelete("{id}")]        
        public IActionResult Excluir([FromRoute] Guid id)
        {
            clienteWorkflow.Delete(id);
            if (clienteWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(clienteWorkflow.GetErrors());
        }
    }
}
