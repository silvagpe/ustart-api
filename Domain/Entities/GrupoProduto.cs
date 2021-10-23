using System;
using UStart.Domain.Commands;

namespace UStart.Domain.Entities
{
    public class GrupoProduto
    {
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public string CodigoExterno { get; private set; }

        public GrupoProduto()
        {            
        }


        /// <summary>
        /// Inserir um grupo
        /// </summary>
        /// <param name="command"></param>
        public GrupoProduto(GrupoProdutoCommand command)
        {
            this.Id = command.Id == Guid.Empty ? Guid.NewGuid() : command.Id;
            this.Descricao = command.Descricao;
            this.CodigoExterno = command.CodigoExterno;
        }

        /// <summary>
        /// Atualizar um grupo
        /// </summary>
        /// <param name="command"></param>
        public void Update(GrupoProdutoCommand command){
            this.Descricao = command.Descricao;
            this.CodigoExterno = command.CodigoExterno;
        }
    }
    
}