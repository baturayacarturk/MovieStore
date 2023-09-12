using Application.Features.Actors.Models;
using Application.Features.SharedDtos;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Commands
{
    public class UpdateActorCommand:IRequest<UpdatedActorViewModel>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<MovieUpdateDto> Movies { get; set; } = new List<MovieUpdateDto>();
    }
}
