using DataAccess.Enum;

namespace BusinessLogic.DTOs.CategoryDTO
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CategorySlug Slug { get; set; }
    }
}
