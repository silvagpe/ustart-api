using System;
using UStart.Domain.Commands;

namespace UStart.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string CodigoExterno { get; private set; }
        public bool Ativo { get; private set; }
        public String Nome { get; private set; }
        public String RazaoSocial { get; private set; }
        public String CNPJ { get; private set; }
        public String CPF { get; private set; }
        public String Rua { get; private set; }
        public String Numero { get; private set; }
        public String Complemento { get; private set; }
        public String Bairro { get; private set; }
        public String EstadoId { get; private set; }
        public String CidadeId { get; private set; }
        public String CidadeNome { get; private set; }
        public String CEP { get; private set; }
        public String Fone { get; private set; }
        public String Email { get; private set; }
        public Decimal LimiteDeCredito { get; private set; }

        public Cliente()
        {
            
        }

        public Cliente(ClienteCommand command)
        {
            Id = command.Id == Guid.Empty ? Guid.NewGuid() : command.Id;   
            AtualizarCampos(command);            
        }

        public void Update(ClienteCommand command)
        {
            AtualizarCampos(command);
        }

        private void AtualizarCampos(ClienteCommand command)
        {            
            CodigoExterno = command.CodigoExterno;                        
            Ativo = command.Ativo;                        
            Nome = command.Nome;                        
            RazaoSocial = command.RazaoSocial;                        
            CNPJ = command.CNPJ;                        
            CPF = command.CPF;                        
            Rua = command.Rua;                        
            Numero = command.Numero;                        
            Complemento = command.Complemento;                        
            Bairro = command.Bairro;                        
            EstadoId = command.EstadoId;
            CidadeId = command.CidadeId;                        
            CidadeNome = command.CidadeNome;                        
            CEP = command.CEP;                        
            Fone = command.Fone;                        
            Email = command.Email;                        
            LimiteDeCredito = command.LimiteDeCredito;                        
        }

    }
    
}