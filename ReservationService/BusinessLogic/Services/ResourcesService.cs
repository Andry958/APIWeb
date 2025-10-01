using AutoMapper;
using BusinessLogic.DTOs.ResourceDTO;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ResourcesService : IResourcesService
    {
        private readonly ReservationServiceDbContext ctx;
        private readonly IMapper mapper;

        public ResourcesService(ReservationServiceDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public ResourceGetDTO Create(ResourceCreateDTO model)
        {
            var entity = mapper.Map<Resource>(model);
            ctx.Resources.Add(entity);
            ctx.SaveChanges(); 
            return mapper.Map<ResourceGetDTO>(entity);
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty) return;

            var item = ctx.Resources.Find(id);
            if (item == null) return;

            ctx.Resources.Remove(item);
            ctx.SaveChanges();
        }

        public void DeleteAll()
        {
            ctx.Resources.RemoveRange(ctx.Resources);
            ctx.SaveChanges();
        }

        public void Edit(ResourceEditDTO model)
        {
            var existingResource = ctx.Resources.Find(model.Id);
            if (existingResource == null) return; 

            mapper.Map(model, existingResource);
            ctx.SaveChanges();
        }

        public ResourceGetDTO? Get(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var item = ctx.Resources.Find(id);
            if (item == null)
                return null;

            return mapper.Map<ResourceGetDTO>(item);
        }

        public IList<ResourceGetDTO> GetAll()
        {
            var items = ctx.Resources
                // .Include(x => x.Reservations) // якщо є зв'язки
                .ToList();

            return mapper.Map<IList<ResourceGetDTO>>(items);
        }
    }
}
