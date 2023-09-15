using Application.Features.Customers.Business;
using Application.Features.Customers.Models;
using Application.Features.Customers.Queries;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Handlers
{
    public class GetCustomerQueryHandler : CustomerUseCase, IRequestHandler<GetCustomerQuery, GetCustomerViewModel>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public GetCustomerQueryHandler(ICustomerRepository repository, IHttpContextAccessor contextAccessor) : base(repository)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<GetCustomerViewModel> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customerId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault().Value;
            var id = EncryptionService.Decrypt(customerId);
            await MustExistsCheckWithId(id);
            var customer = await Repository.Get(x => x.Id == id);
            GetCustomerViewModel response = new()
            {
                Id = EncryptionService.Encrypt(id),
                FirstName = customer.FirstName,
                LastName = customer.LastName,

            };
            return response;    
        }
    }
}
