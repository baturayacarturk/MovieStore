using Application.Features.Actors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Models
{
    public class GetListActorQueryViewModel
    {
        public ICollection<GetListActorQueryDto> Actors{ get; set; }=new List<GetListActorQueryDto>();  
    }
}
