using DataAccess.Enum;

namespace DataAccess.Data.Entities
{
    public class Category : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CategorySlug Slug { get; set; }
        public IEnumerable<Resource>? Resources { get; set; }
    }
}
