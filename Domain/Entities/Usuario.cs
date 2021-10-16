
using System;
using UStart.Domain.Commands;
using UStart.Domain.Helpers;

namespace UStart.Domain.Entities
{
    // OrganizationEntity inherits from EntityBase and have all base props + "OrganizationId"
    public class Usuario 
    {
        public Guid Id { get; private set; }    
        public Boolean Ativo { get; private set; }    
        public string Nome { get; private set; }    
        public string Email { get; private set; }    
        public String Autenticacao { get; private set; }    
        public DateTime DataRegistro { get; private set; }    
        
        public Usuario()
        {
            
        }

        public Usuario(string email, string senha)
        {
            DataRegistro = DateTime.Now;
            Email = email;
            Ativo = true;
            Autenticacao = Md5Helper.GenerateHash($"{Email}:{senha}");
        }
        public Usuario(UsuarioCommand command){
            AtualizarCampos(command);
            this.DataRegistro = DateTime.Now;
            Autenticacao = Md5Helper.GenerateHash($"{command.Email}:{command.Senha}");
        }

        public void Update(UsuarioCommand command)
        {            
            AtualizarCampos(command);  
            if (!string.IsNullOrEmpty(command.Senha)){
                Autenticacao = Md5Helper.GenerateHash($"{command.Email}:{command.Senha}");
            }
        }

        private void AtualizarCampos(UsuarioCommand command){
            Id = command.Id;
            Email = command.Email;
            Ativo = command.Ativo;
            Nome = command.Nome;            
        }

    }
}
