using Azure.Core.Pipeline;
using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.CategoryDTO
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; }
        public CategorySlug Slug { get; set; }
    }
}
