using BusinessLogic.DTOs.CategoryDTO;
using DataAccess.Enum;

namespace BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        Task SeedCategories();
        Task<IList<CategoryDTO>> GetAll(string? ByName, CategorySlug categorySlug, int pageNumber);
        Task<CategoryDTO?> Get(Guid id);
        Task<CategoryDTO> Create(CategoryCreateDTO model);
        Task<CategoryDTO> Edit(CategoryDTO model);
        Task Delete(Guid id);
        Task DeleteAll();
    }
}
