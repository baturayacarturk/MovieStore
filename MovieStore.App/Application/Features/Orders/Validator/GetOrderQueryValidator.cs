using Application.Features.Orders.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Validator
{
    public class GetOrderQueryValidator:AbstractValidator<GetOrderQuery>
    {
        public GetOrderQueryValidator()
        {
            RuleFor(c=>c.Id).NotNull().NotEmpty();
        }
    }
}
