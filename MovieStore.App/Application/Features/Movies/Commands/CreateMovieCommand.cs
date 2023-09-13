using Application.Features.Actors.Dtos;
using Application.Features.Movies.Models;
using Application.Features.SharedDtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Commands
{
    public class CreateMovieCommand:IRequest<CreatedMovieViewModel>
    {
        public string Name { get; set; }
        public MovieType Type { get; set; }
        public string DirectorId { get; set; }
        public string? CustomerId { get; set; }
        public ICollection<ActorsDto> Actors { get; set; } = new List<ActorsDto>();
        public int Price { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
