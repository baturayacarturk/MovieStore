using Application.Features.Orders.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands
{
    public class CreateOrderCommand:IRequest<CreatedOrderViewModel>
    {
        public string MovieId { get; set; }
    }
}
