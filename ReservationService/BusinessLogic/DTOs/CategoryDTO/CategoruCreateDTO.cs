using DataAccess.Enum;

namespace BusinessLogic.DTOs.CategoryDTO
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; }
        public CategorySlug Slug { get; set; }
    }
}
