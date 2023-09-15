using Application.Features.Customers.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands
{
    public class CreateCustomerCommand:IRequest<CreatedCustomerViewModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
