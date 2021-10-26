using System;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.UoW;

namespace UStart.Domain.Workflows
{
    public class ClienteWorkflow : WorkflowBase
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ClienteWorkflow(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
        {
            this.clienteRepository = clienteRepository;
            _unitOfWork = unitOfWork;
        }

        public Cliente Add(ClienteCommand command)
        {

            if (string.IsNullOrEmpty(command.Nome))
            {
                this.AddError("Nome", "Nome não informado");
            }

            if (this.IsValid() == false)
            {
                return null;
            }

            var cliente = new Cliente(command);
            clienteRepository.Add(cliente);
            _unitOfWork.Commit();

            return cliente;
        }

        public void Update(Guid id, ClienteCommand command){
            
            var cliente = clienteRepository.ConsultarPorId(id);
            if (cliente != null){
                cliente.Update(command);
                clienteRepository.Update(cliente);
                _unitOfWork.Commit();
            }
            else{
                AddError("Cliente", "Cliente não pode ser encontrado", id);
            }

        }

        public void Delete(Guid id){

            //Cliente Cliente = _ClienteRepository.ConsultarPorId(id);

            var cliente = clienteRepository.ConsultarPorId(id);
            if (cliente != null){
                clienteRepository.Delete(cliente);
                _unitOfWork.Commit();                
            }else{
                AddError("Cliente", "Cliente não pode ser encontrado", id);
            }            
        }
    }
}