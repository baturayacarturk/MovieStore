using Application.Features.Customers.Business;
using Application.Features.Customers.Commands;
using Application.Features.Customers.Models;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Handlers
{
    public class CreateCustomerCommandHandler : CustomerUseCase, IRequestHandler<CreateCustomerCommand, CreatedCustomerViewModel>
    {
        public CreateCustomerCommandHandler(ICustomerRepository repository) : base(repository)
        {
        }

        public async Task<CreatedCustomerViewModel> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            var createdCustomer = await Repository.AddAsync(customer);
            CreatedCustomerViewModel response = new()
            {
                Id = EncryptionService.Encrypt(createdCustomer.Id),
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            return response;
        }
    }
}
