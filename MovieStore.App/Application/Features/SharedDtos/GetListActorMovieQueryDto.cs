using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SharedDtos
{
    public class GetListActorMovieQueryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public MovieType Type { get; set; }
        public string DirectorId { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; }
    }
}
