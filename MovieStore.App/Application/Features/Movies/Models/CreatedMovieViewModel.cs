using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Models
{
    public class CreatedMovieViewModel
    {
        public string Name { get; set; }
        public MovieType Type { get; set; }
        public string DirectorId { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
