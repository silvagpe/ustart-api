using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Workflows;

namespace UStart.API.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/produto")]
    [Authorize]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly ProdutoWorkflow produtoWorkflow;

        public ProdutoController(IProdutoRepository produtoRepository, ProdutoWorkflow produtoWorkflow)
        {
            this.produtoRepository = produtoRepository;
            this.produtoWorkflow = produtoWorkflow;
        }

        /// <summary>
        /// Consultar todos os grupos
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult Get([FromQuery] string pesquisa)
        {
            return Ok(produtoRepository.Pesquisar(pesquisa));
        }

                /// <summary>
        /// Consultar apenas um grupo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]    
        [Route("{id}")]    
        public IActionResult GetPorId([FromRoute] Guid id)
        {
            return Ok(produtoRepository.ConsultarPorId(id));
        }


        /// <summary>
        /// Método para inserir no banco um regitro de grupo produto
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Adicionar([FromBody] ProdutoCommand command )
        {
            produtoWorkflow.Add(command);
            if (produtoWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(produtoWorkflow.GetErrors());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Atualizar([FromRoute] Guid id, [FromBody] ProdutoCommand command )
        {
            produtoWorkflow.Update(id, command);
            if (produtoWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(produtoWorkflow.GetErrors());
        }

        [HttpDelete("{id}")]        
        public IActionResult Excluir([FromRoute] Guid id)
        {
            produtoWorkflow.Delete(id);
            if (produtoWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(produtoWorkflow.GetErrors());
        }
    }
}
