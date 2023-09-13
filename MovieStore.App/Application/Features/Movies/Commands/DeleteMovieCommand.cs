using Application.Features.Movies.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Commands
{
    public class DeleteMovieCommand:IRequest<DeletedMovieViewModel>
    {
        public string Id { get; set; }
    }
}
