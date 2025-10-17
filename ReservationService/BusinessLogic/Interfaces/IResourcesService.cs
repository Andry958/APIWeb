using BusinessLogic.DTOs.ResourceDTO;

namespace BusinessLogic.Interfaces
{
    public interface IResourcesService
    {
        Task SeedResources();
        Task<IList<ResourceGetDTO>> GetAll(Guid? filterCategoryId, string? ByName, string? ByDescription, decimal? filterMin, decimal? filterMax,bool? SortPriceAsc, int pageNumber);
        Task<ResourceGetDTO?> Get(Guid id);
        Task<ResourceGetDTO> Create(ResourceCreateDTO model);
        Task Edit(ResourceEditDTO model);
        Task Delete(Guid id);
        Task DeleteAll();
    }
    
}
