using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Dtos
{
    public class GetListOrderDto
    {
        public string Id { get; set; }
        public string MovieName { get; set; }
        public string Date{ get; set; }
        public int Price { get; set; }

    }
}
