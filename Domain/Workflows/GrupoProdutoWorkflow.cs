using System;
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

        public void Update(Guid id, GrupoProdutoCommand command){
            
            var grupoProduto = _grupoProdutoRepository.ConsultarPorId(id);
            if (grupoProduto != null){
                grupoProduto.Update(command);
                _grupoProdutoRepository.Update(grupoProduto);
                _unitOfWork.Commit();
            }
            else{
                AddError("Grupo produto", "Grupo produto não pode ser encontrado", id);
            }

        }

        public void Delete(Guid id){

            //GrupoProduto grupoProduto = _grupoProdutoRepository.ConsultarPorId(id);

            var grupoProduto = _grupoProdutoRepository.ConsultarPorId(id);
            if (grupoProduto != null){
                _grupoProdutoRepository.Delete(grupoProduto);
                _unitOfWork.Commit();                
            }else{
                AddError("Grupo produto", "Grupo produto não pode ser encontrado", id);
            }            
        }
    }
}