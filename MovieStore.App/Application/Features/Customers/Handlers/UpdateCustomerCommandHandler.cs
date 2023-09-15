using Application.Features.Customers.Business;
using Application.Features.Customers.Commands;
using Application.Features.Customers.Models;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Handlers
{
    public class UpdateCustomerCommandHandler : CustomerUseCase, IRequestHandler<UpdateCustomerCommand, UpdatedCustomerViewModel>
    {
        public UpdateCustomerCommandHandler(ICustomerRepository repository) : base(repository)
        {
        }

        public async Task<UpdatedCustomerViewModel> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var id = EncryptionService.Decrypt(request.Id);
            await MustExistsCheckWithId(id);
            var customer = await Repository.Get(x => x.Id == id);
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            var updatedCustomer = await Repository.UpdateAsync(customer);
            UpdatedCustomerViewModel response = new()
            {
                FirstName = updatedCustomer.FirstName,
                LastName = updatedCustomer.LastName,
                Message = "Customer information has updated"
            };
            return response;
        }
    }
}
