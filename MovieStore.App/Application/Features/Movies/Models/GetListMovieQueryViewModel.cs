using Application.Features.Directors.Dtos;
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
    public class GetListMovieQueryViewModel
    {
        public ICollection<GetListMovieQueryDto> Movies { get; set; } = new List<GetListMovieQueryDto>();

    }
}
