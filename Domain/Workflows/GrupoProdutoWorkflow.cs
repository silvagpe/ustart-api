using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.UoW;

namespace UStart.Domain.Workflows
{
    public class GrupoProdutoWorkflow : WorkflowBase
    {
        private readonly IGrupoProdutoRepository _grupoProdutoRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GrupoProdutoWorkflow(IGrupoProdutoRepository grupoProdutoReposotory, IUnitOfWork unitOfWork)
        {
            _grupoProdutoRepository = grupoProdutoReposotory;
            _unitOfWork = unitOfWork;
        }

        public GrupoProduto Add(GrupoProdutoCommand command)
        {

            if (string.IsNullOrEmpty(command.Descricao))
            {
                this.AddError("Descricao", "Descrição não informada");
            }

            if (this.IsValid() == false)
            {
                return null;
            }

            var grupoProduto = new GrupoProduto(command);
            _grupoProdutoRepository.Add(grupoProduto);
            _unitOfWork.Commit();

            return grupoProduto;
        }
    }
}