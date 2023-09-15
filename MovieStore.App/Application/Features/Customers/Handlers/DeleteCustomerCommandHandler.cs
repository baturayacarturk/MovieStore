using Application.Features.Customers.Business;
using Application.Features.Customers.Commands;
using Application.Features.Customers.Models;
using Application.Services.Repositories;
using Core.Application;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Handlers
{
    public class DeleteCustomerCommandHandler : CustomerUseCase, IRequestHandler<DeleteCustomerCommand, DeletedCustomerViewModel>
    {
        public DeleteCustomerCommandHandler(ICustomerRepository repository) : base(repository)
        {
        }

        public async Task<DeletedCustomerViewModel> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var id = EncryptionService.Decrypt(request.Id);
            await MustExistsCheckWithId(id);
            var customer = await Repository.Get(x => x.Id == id);
            customer.IsActive = false;
            var deletedCustomer = await Repository.UpdateAsync(customer);
            DeletedCustomerViewModel response = new()
            {
                FirstName = deletedCustomer.FirstName,
                LastName = deletedCustomer.LastName,
                Message = Messages.DeletedMessage
            };
            return response;
        }
    }
}
