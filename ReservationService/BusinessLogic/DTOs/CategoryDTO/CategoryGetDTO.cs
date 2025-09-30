using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.CategoryDTO
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
