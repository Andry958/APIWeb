using DataAccess.Enum;

namespace BusinessLogic.DTOs.ResourceDTO
{
    public class ResourceGetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public string ImageUrl { get; set; }
        public CategorySlug CategorySlug { get; set; }
        public Guid CategoryId { get; set; }
    }
}
