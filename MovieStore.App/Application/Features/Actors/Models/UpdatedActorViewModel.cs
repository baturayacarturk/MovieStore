using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Models
{
    public class UpdatedActorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Movie> Movies { get; set; }
        public string UpdatedFirstName { get; set; }
        public string UpdatedLastName { get; set; }
        public bool IsMoviesUpdated { get; set; }
        public List<Movie> UpdatedMovies { get; set; }
    }
}
