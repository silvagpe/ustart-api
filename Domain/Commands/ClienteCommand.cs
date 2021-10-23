using System;

namespace UStart.Domain.Commands
{
    public class ClienteCommand
    {
        public Guid Id { get; set; }
        public string CodigoExterno { get; set; }
        public bool Ativo { get; set; }
        public String Nome { get; set; }
        public String RazaoSocial { get; set; }
        public String CNPJ { get; set; }
        public String CPF { get; set; }
        public String Rua { get; set; }
        public String Numero { get; set; }
        public String Complemento { get; set; }
        public String Bairro { get; set; }
        public String EstadoId { get; set; }
        public String CidadeId { get; set; }
        public String CidadeNome { get; set; }
        public String CEP { get; set; }
        public String Fone { get; set; }
        public String Email { get; set; }
        public Decimal LimiteDeCredito { get; set; }
        
    }

}