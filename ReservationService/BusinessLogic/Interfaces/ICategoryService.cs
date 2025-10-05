using BusinessLogic.DTOs.CategoryDTO;
using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        void SeedCategories();
        IList<CategoryDTO> GetAll(string? ByName, CategorySlug categorySlug);
        CategoryDTO? Get(Guid id);
        CategoryDTO Create(CategoryCreateDTO model);
        CategoryDTO Edit(CategoryDTO model);
        void Delete(Guid id);
        void DeleteAll();
    }
}
