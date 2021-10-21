using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UStart.Domain.Contracts.Repositories;

namespace UStart.API.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/grupo-produto")]
    [Authorize]
    public class GrupoProdutoController : ControllerBase
    {
        private readonly IGrupoProdutoRepository grupoProdutoRepository;

        public GrupoProdutoController(IGrupoProdutoRepository grupoProdutoRepository)
        {
            this.grupoProdutoRepository = grupoProdutoRepository;
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

    }
}
