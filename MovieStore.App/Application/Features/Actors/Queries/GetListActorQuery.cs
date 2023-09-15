using Application.Features.Actors.Models;
using Core.Application.Pipelines.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Queries
{
    public class GetListActorQuery:IRequest<GetListActorQueryViewModel>,ILoggableRequest
    {
    }
}
