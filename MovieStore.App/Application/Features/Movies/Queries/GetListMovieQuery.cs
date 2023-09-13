using Application.Features.Movies.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Queries
{
    public class GetListMovieQuery:IRequest<GetListMovieQueryViewModel>
    {
    }
}
