using Application.BaseUseCase;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Business
{
    public class OrderUseCase : BaseUseCase<Order, IOrderRepository>
    {
        public OrderUseCase(IOrderRepository repository) : base(repository)
        {
        }
        public override async Task MustExistsCheckWithId(int orderId)
        {
            var order = await Repository.Get(x => x.Id == orderId);
            if (order is null) throw new BusinessException("Order could not be found");
        }
        public async Task CheckIfOrderIsBelongToCustomer(int customerId, int orderId)
        {
            var order = await Repository.Get(x=>x.CustomerId == customerId&&x.Id==orderId);
            if (order is null) throw new BusinessException("Order could not be found");
        }
    }
}
