using Application.Features.Orders.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries
{
    public class GetOrderListQuery:IRequest<GetOrderListQueryViewModel>
    {
    }
}
