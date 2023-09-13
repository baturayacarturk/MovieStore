using Application.Features.Movies.Dtos;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Models
{
    public class UpdatedMovieViewModel
    {
        public string Name { get; set; }
        public string? UpdatedName { get; set; }
        public string DirectorId { get; set; }
        public string? UpdatedDirectorId { get; set; }
        public MovieType Type { get; set; }
        public MovieType? UpdatedMovieType { get; set; }
        public ICollection<UpdatedMovieCommandActorList> Actors { get; set; } = new List<UpdatedMovieCommandActorList>();
        public ICollection<UpdatedMovieCommandActorList>? UpdatedActors { get; set; } = new List<UpdatedMovieCommandActorList>();
        public int Price { get; set; }
        public int? UpdatedPrice { get; set; }
    }
}

