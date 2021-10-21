using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UStart.API.Controllers
{
    /// <summary>
    /// Exemplo de controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/grupo-produto-exemplo")]
    [Authorize]
    public class GrupoProdutoExemploController : ControllerBase
    {
        public GrupoProdutoExemploController()
        {            
        }

        /// <summary>
        /// Consultar todos os grupos
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult GetGruposProdutos()
        {
            return Ok("consulta de grupos");
        }

        /// <summary>
        /// Consultar apenas um grupo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]    
        [Route("{id}")]    
        public IActionResult GetGrupoProduto([FromRoute] string id)
        {
            return Ok(string.Format("consulta um grupo: {0}", id));
        }

        /// <summary>
        /// Adicionar (insert) um grupo
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]            
        public IActionResult AdicionarGrupoProduto([FromBody] dynamic command)
        {
            return Ok(string.Format("adicionar um grupo: {0}", command));
        }

        /// <summary>
        /// Atualizar (update) um grupo
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]            
        public IActionResult AtualizarGrupoProduto([FromBody] dynamic command)
        {
            return Ok(string.Format("atualizar um grupo: {0}", command));
        }

        /// <summary>
        /// Excluir um grupo por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]            
        public IActionResult DeletarGrupoProduto([FromRoute] string id)
        {
            return Ok(string.Format("excluir um grupo: {0}", id));
        }


    }
}
