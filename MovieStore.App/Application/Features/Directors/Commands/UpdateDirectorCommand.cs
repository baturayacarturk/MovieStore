using Application.Features.Directors.Models;
using Application.Features.SharedDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Directors.Commands
{
    public class UpdateDirectorCommand : IRequest<UpdatedDirectorViewModel>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<MovieUpdateDto> Movies { get; set; } = new List<MovieUpdateDto>();
    }
}
