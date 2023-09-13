using Application.Features.SharedDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Dtos
{
    public class GetListActorQueryDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<GetListActorMovieQueryDto> Movies { get; set; } = new List<GetListActorMovieQueryDto>();

    }
}
