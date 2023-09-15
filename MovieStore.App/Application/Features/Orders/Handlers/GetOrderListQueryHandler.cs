using Application.Features.Orders.Business;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Features.Orders.Queries;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Handlers
{
    public class GetOrderListQueryHandler : OrderUseCase, IRequestHandler<GetOrderListQuery, GetOrderListQueryViewModel>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public GetOrderListQueryHandler(IOrderRepository repository, IHttpContextAccessor httpContextAccessor) : base(repository)
        {
            _contextAccessor = httpContextAccessor;
        }

        public async Task<GetOrderListQueryViewModel> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var customerId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault().Value;
            var orders = await Repository.GetList(x=>x.CustomerId==EncryptionService.Decrypt(customerId),include:x=>x.Include(x=>x.Movie));

            GetOrderListQueryViewModel response = new();
            foreach(var item in orders)
            {
                response.Orders.Add(new GetListOrderDto
                {
                    Date = item.OrderDate.ToString("dd/MM/yyyy"),
                    Id = EncryptionService.Encrypt(item.Id),
                    MovieName = item.Movie.Name,
                    Price = item.Price,
                });
            }
            return response;
        }
    }
}
