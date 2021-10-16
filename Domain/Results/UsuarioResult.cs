using System;
using UStart.Domain.Entities;

namespace UStart.Domain.Results
{
    public class UsuarioResult
    {
        public Guid Id { get; set; }    
        public Boolean Ativo { get; set; }    
        public string Nome { get; set; }    
        public string Email { get; set; }        
        public DateTime DataRegistro { get; set; }   

        public UsuarioResult(Usuario usuario)
        {
            this.Id = usuario.Id;
            this.Ativo = usuario.Ativo;
            this.Nome = usuario.Nome;
            this.Email = usuario.Email;
            this.DataRegistro = usuario.DataRegistro;           
        }
    }
}