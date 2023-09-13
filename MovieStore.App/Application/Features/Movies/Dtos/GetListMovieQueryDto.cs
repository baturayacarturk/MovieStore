using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Dtos
{
    public class GetListMovieQueryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public MovieType Type { get; set; }
        public string DirectorId { get; set; }
        public string? CustomerId { get; set; }
        public ICollection<GetListMovieQueryActorDto> Actors { get; set; } = new List<GetListMovieQueryActorDto>();
        public int Price { get; set; }
        public bool IsActive { get; set; }
    }
}
