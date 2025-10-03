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
using Microsoft.EntityFrameworkCore;
using DataAccess.Enum;

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

        public IList<ResourceGetDTO> GetAll(Guid? filterCategoryId, string? ByName, string? ByDescription, decimal? filterMin, decimal? filterMax, bool? SortPriceAsc)
        {
            IQueryable<Resource> query = ctx.Resources.Include(x => x.Category);

            if (filterCategoryId != null)
                query = query.Where(x => x.CategoryId == filterCategoryId);

            if (ByName != null)
                query = query.Where(x => x.Name.ToLower().Contains(ByName.ToLower()));

            if (ByDescription != null)
                query = query.Where(x => x.Name.ToLower().Contains(ByDescription.ToLower()));

            if (filterMin != null && filterMax != null)
                query = query.Where(x => x.PricePerHour >= filterMin && x.PricePerHour <= filterMax);

            if (SortPriceAsc != null)
                if (SortPriceAsc == true)
                    query = query.OrderBy(x => x.PricePerHour);
                if(SortPriceAsc == false)
                    query = query.OrderByDescending(x => x.PricePerHour);




            var items = query.ToList();

            return mapper.Map<IList<ResourceGetDTO>>(items);
        }
    }
}
