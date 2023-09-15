using Application.Features.Orders.Models;
using Core.Application.Pipelines.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries
{
    public class GetOrderQuery:IRequest<GetOrderQueryViewModel>,ILoggableRequest
    {
        public string Id { get; set; }
    }
}
