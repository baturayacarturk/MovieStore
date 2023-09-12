using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Directors.Models
{
    public class DeletedDirectorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Message { get; set; }
    }
}
