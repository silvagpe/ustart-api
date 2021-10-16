using System;
using System.Collections.Generic;
using UStart.Domain.Entities;
using UStart.Domain.Results;

namespace UStart.Domain.Contracts.Repositories
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);
        IEnumerable<UsuarioResult> GetAll();
        IEnumerable<UsuarioResult> Pesquisar(string pesquisa);
        Usuario GetByEmail(string email);
        Usuario GetById(Guid id);
        Usuario Login(string hashMd5);
        void Update(Usuario usuario);
    }
}
