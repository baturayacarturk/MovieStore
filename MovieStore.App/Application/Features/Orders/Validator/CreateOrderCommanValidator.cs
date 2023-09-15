using Application.Features.Orders.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Validator
{
    public class CreateOrderCommanValidator:AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommanValidator()
        {
            RuleFor(c=>c.MovieId).NotEmpty().MinimumLength(3).NotNull();
        }
    }
}
