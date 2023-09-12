using Application.Features.Actors.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Commands
{
    public class CreateActorCommand:IRequest<CreatedActorViewModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
