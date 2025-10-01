using BusinessLogic.DTOs.ResourceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IResourcesService
    {
        IList<ResourceGetDTO> GetAll();
        ResourceGetDTO? Get(Guid id);
        ResourceGetDTO Create(ResourceCreateDTO model);
        void Edit(ResourceEditDTO model);
        void Delete(Guid id);
        void DeleteAll();
    }
}
