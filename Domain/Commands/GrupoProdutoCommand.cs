using System;

namespace UStart.Domain.Commands
{
    public class GrupoProdutoCommand
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string CodigoExterno { get; set; }
        
    }
}