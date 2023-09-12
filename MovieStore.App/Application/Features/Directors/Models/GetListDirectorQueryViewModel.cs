using Application.Features.Actors.Dtos;
using Application.Features.Directors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Directors.Models
{
    public class GetListDirectorQueryViewModel
    {
        public ICollection<GetListDirectorQueryDto> Directors { get; set; } = new List<GetListDirectorQueryDto>();
    }
}
