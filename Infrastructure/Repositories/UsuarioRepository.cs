using System;
using System.Collections.Generic;
using System.Linq;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.Results;
using UStart.Infrastructure.Context;

namespace UStart.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UStartContext _context;

        public UsuarioRepository(UStartContext context)
        {
            _context = context;
        }

        public Usuario Login(string hashMd5)
        {
            return _context.Usuarios
                .Where(u => u.Autenticacao == hashMd5 && u.Ativo)
                .FirstOrDefault();
        }
        public Usuario GetByEmail(string email)
        {
            return _context.Usuarios
                .Where(u => u.Email.ToLower() == email.ToLower() && u.Ativo)
                .FirstOrDefault();
        }
        public Usuario GetById(Guid id)
        {
            return _context.Usuarios
                .FirstOrDefault(u => u.Id == id);
        }

        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public IEnumerable<UsuarioResult> GetAll()
        {
            return _context.Usuarios.Select(usuario => new UsuarioResult(usuario)).ToList();
        }
        public IEnumerable<UsuarioResult> Pesquisar(string pesquisa)
        {
            pesquisa = pesquisa.ToLower();
            return _context
            .Usuarios
            .Where(
                x => x.Email.ToLower().Contains(pesquisa)
                || x.Nome.Contains(pesquisa))
            .Select(usuario => new UsuarioResult(usuario)).ToList();
        }
    }
}
