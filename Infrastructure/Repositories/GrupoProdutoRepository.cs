using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Infrastructure.Context;

namespace UStart.Infrastructure.Repositories
{
    public class GrupoProdutoRepository : IGrupoProdutoRepository
    {
        private readonly UStartContext _context;

        public GrupoProdutoRepository(UStartContext context)
        {
            _context = context;
        }

        public GrupoProduto ConsultarPorId(Guid id)
        {
            return _context.GrupoProdutos.FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<GrupoProduto> Pesquisar(string pesquisa)
        {
            pesquisa = pesquisa != null ? pesquisa.ToLower() : string.Empty;

            return _context
            .GrupoProdutos
            .Where(x => x.Descricao.ToLower().Contains(pesquisa))
            .ToList();
        }

        public void Add(GrupoProduto grupoProduto)
        {
            _context.GrupoProdutos.Add(grupoProduto);            
        }

        public void Update(GrupoProduto grupoProduto)
        {
            _context.GrupoProdutos.Update(grupoProduto);
        }

        public void Delete(GrupoProduto grupoProduto)
        {
            if (_context.Entry(grupoProduto).State == EntityState.Detached)
            {
                _context.GrupoProdutos.Attach(grupoProduto);
            }
            _context.GrupoProdutos.Remove(grupoProduto);
        }
    }
}