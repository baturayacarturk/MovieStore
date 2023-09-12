using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        public int MovieId { get; set; }
        [JsonIgnore]
        public Movie Movie { get; set; }

        public int Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
