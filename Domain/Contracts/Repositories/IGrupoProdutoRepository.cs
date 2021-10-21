using System;
using System.Collections.Generic;
using UStart.Domain.Entities;

namespace UStart.Domain.Contracts.Repositories
{
    public interface IGrupoProdutoRepository
    {
        GrupoProduto ConsultarPorId(Guid id);
        IEnumerable<GrupoProduto> Pesquisar(string pesquisa);
    }
}