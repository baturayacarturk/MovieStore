using Application.Features.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Models
{
    public class GetOrderQueryViewModel
    {
        public string Id { get; set; }
        public CreateOrderMovieDto Movie { get; set; }
        public string OrderDate { get; set; }
    }
}
