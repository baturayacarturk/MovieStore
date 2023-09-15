using Application.Features.Tokens.Models;
using Application.Features.Users.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetRefreshTokenQuery:IRequest<GetRefreshTokenViewModel>
    {
        public string Token { get; set; }
    }
}
