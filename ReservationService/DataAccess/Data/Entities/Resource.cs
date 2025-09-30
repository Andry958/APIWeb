using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public decimal PricePerHour { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public IEnumerable<Category>? Category { get; set; }
    }

}
