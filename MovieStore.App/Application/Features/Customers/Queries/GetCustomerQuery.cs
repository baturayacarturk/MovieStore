using Application.Features.Customers.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries
{
    public class GetCustomerQuery:IRequest<GetCustomerViewModel>
    {
        //public string Id { get; set; }
    }
}
