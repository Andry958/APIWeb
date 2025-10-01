using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CategorySlug Slug { get; set; }
        public IEnumerable<Resource>? Resources { get; set; }
    }
}
