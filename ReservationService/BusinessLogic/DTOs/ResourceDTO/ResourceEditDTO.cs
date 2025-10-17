namespace BusinessLogic.DTOs.ResourceDTO
{
    public class ResourceEditDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
    }
}
