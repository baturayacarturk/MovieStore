using Application.Features.Customers.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands
{
    public class DeleteCustomerCommand:IRequest<DeletedCustomerViewModel>
    {
        public string Id { get; set; }
    }
}
