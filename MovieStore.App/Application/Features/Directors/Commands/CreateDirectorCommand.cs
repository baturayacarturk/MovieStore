using Application.Features.Directors.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Directors.Commands
{
    public class CreateDirectorCommand:IRequest<CreatedDirectorViewModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

