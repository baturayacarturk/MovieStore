using Application.Features.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Models
{
    public class GetOrderListQueryViewModel
    {
        public List<GetListOrderDto> Orders { get; set; } = new List<GetListOrderDto>();
    }
}
