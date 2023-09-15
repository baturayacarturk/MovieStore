using Application.BaseUseCase;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Business
{
    public class CustomerUseCase : BaseUseCase<Customer, ICustomerRepository>
    {
        public CustomerUseCase(ICustomerRepository repository) : base(repository)
        {
        }
        public override async Task MustExistsCheckWithId(int id)
        {
            var customer = await Repository.Get(x => x.Id == id);
            if (customer is null) throw new BusinessException("Customer is not exists.");

        }
    }
}
