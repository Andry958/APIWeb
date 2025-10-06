using BusinessLogic.DTOs.CategoryDTO;
using BusinessLogic.DTOs.ResourceDTO;
using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
