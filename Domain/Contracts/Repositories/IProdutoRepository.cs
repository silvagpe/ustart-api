using System;
using System.Collections.Generic;
using UStart.Domain.Entities;

namespace UStart.Domain.Contracts.Repositories
{
    public interface IProdutoRepository
    {
        void Add(Produto Produto);
        Produto ConsultarPorId(Guid id);
        void Delete(Produto Produto);
        IEnumerable<Produto> Pesquisar(string pesquisa);
        void Update(Produto Produto);
    }

}