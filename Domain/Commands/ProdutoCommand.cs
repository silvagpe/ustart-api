using System;

namespace UStart.Domain.Commands
{
    public class ProdutoCommand
    {
        public Guid Id { get; set; }
        public Guid GrupoProdutoId { get; set; }        
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string UrlImagem { get; set; }
        public string CodigoExterno { get; set; }
    }
}