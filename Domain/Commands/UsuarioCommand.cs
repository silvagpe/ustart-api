using System;

namespace UStart.Domain.Commands
{
    public class UsuarioCommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }

        public UsuarioCommand() { }

        public UsuarioCommand(
            string email,
            string senha,
            bool ativo)
        {
            this.Email = email;
            this.Senha = senha;
            this.Ativo = ativo;
        }

    }
}