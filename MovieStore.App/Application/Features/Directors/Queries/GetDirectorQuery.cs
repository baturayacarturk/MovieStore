using Application.Features.Directors.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Directors.Queries
{
    public class GetDirectorQuery:IRequest<GetDirectorQueryViewModel>
    {
        public string Id { get; set; }
    }
}
