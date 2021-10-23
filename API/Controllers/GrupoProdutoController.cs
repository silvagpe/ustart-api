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
    [Route("api/v{version:apiVersion}/grupo-produto")]
    [Authorize]
    public class GrupoProdutoController : ControllerBase
    {
        private readonly IGrupoProdutoRepository grupoProdutoRepository;
        private readonly GrupoProdutoWorkflow grupoProdutoWorkflow;

        public GrupoProdutoController(IGrupoProdutoRepository grupoProdutoRepository, GrupoProdutoWorkflow grupoProdutoWorkflow)
        {
            this.grupoProdutoRepository = grupoProdutoRepository;
            this.grupoProdutoWorkflow = grupoProdutoWorkflow;
        }

        /// <summary>
        /// Consultar todos os grupos
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult GetGruposProdutos([FromQuery] string pesquisa)
        {
            return Ok(grupoProdutoRepository.Pesquisar(pesquisa));
        }

                /// <summary>
        /// Consultar apenas um grupo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]    
        [Route("{id}")]    
        public IActionResult GetGrupoProduto([FromRoute] Guid id)
        {
            return Ok(grupoProdutoRepository.ConsultarPorId(id));
        }


        /// <summary>
        /// Método para inserir no banco um regitro de grupo produto
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AdicionarGrupoProduto([FromBody] GrupoProdutoCommand command )
        {
            grupoProdutoWorkflow.Add(command);
            if (grupoProdutoWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(grupoProdutoWorkflow.GetErrors());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult AtualizarGrupoProduto([FromRoute] Guid id, [FromBody] GrupoProdutoCommand command )
        {
            grupoProdutoWorkflow.Update(id, command);
            if (grupoProdutoWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(grupoProdutoWorkflow.GetErrors());
        }

        [HttpDelete("{id}")]        
        public IActionResult ExcluirGrupoProduto([FromRoute] Guid id)
        {
            grupoProdutoWorkflow.Delete(id);
            if (grupoProdutoWorkflow.IsValid()){
                return Ok();
            }
            return BadRequest(grupoProdutoWorkflow.GetErrors());
        }
    }
}
