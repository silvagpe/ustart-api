using System;

namespace UStart.Domain.Entities
{
    public class GrupoProduto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string CodigoExterno { get; set; }

        public GrupoProduto()
        {            
        }
    }
    
}