using Core.Security.Features.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Features.Commands
{
    public class CreateUserCommand:IRequest<CreatedUserCommand>
    {

    }
}
