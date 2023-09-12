using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MovieType Type { get; set; }
        public int DirectorId { get; set; }
        [JsonIgnore]
        public Director Director { get; set; }
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
        public int Price { get; set; }
        public bool IsActive { get; set; }
    }
}
