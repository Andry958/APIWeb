using AutoMapper;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using DataAccess.Enum;
using BusinessLogic.DTOs.ResourceDTO;
using BusinessLogic.Helpers;
using DataAccess.Repositories;
using LinqKit;

namespace BusinessLogic.Services
{
    public class ResourcesService : IResourcesService
    {
        private readonly ReservationServiceDbContext ctx;
        private readonly IMapper mapper;
        private readonly IRepository<Resource> repo;

        public ResourcesService(ReservationServiceDbContext ctx, IMapper mapper, IRepository<Resource> repo)
        {
            this.repo = repo;
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public async Task<ResourceGetDTO> Create(ResourceCreateDTO model)
        {
            var entity = mapper.Map<Resource>(model);

            var category = await ctx.Categories
                                    .Where(c => c.Id == model.CategoryId)
                                    .Select(c => c.Slug)
                                    .FirstOrDefaultAsync();

            if (category == null)
                throw new Exception("Категорія не знайдена.");

            entity.CategorySlug = category;

            //await ctx.Resources.AddAsync(entity);
            //await ctx.SaveChangesAsync();

            await repo.AddAsync(entity);

            return mapper.Map<ResourceGetDTO>(entity);
        }
        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty) return;

            var item = await repo.GetByIdAsync(id);
            //var item = ctx.Resources.Find(id);
            if (item == null) return;

            //ctx.Resources.Remove(item);
            //await ctx.SaveChangesAsync();

            await repo.DeleteAsync(item);
        }

        public async Task DeleteAll()
        {
            //ctx.Resources.RemoveRange(ctx.Resources);
            //await ctx.SaveChangesAsync();

            await repo.ClearAsync();

        }

        public async Task Edit(ResourceEditDTO model)
        {
            if (model == null) return; 
            await repo.UpdateAsync(mapper.Map<Resource>(model));
        }

        public async Task<ResourceGetDTO?> Get(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var item = await repo.GetByIdAsync(id);

            if (item == null)
                return null;

            return mapper.Map<ResourceGetDTO>(item);
        }

        public async Task<IList<ResourceGetDTO>> GetAll(
            Guid? filterCategoryId,
            string? ByName,
            string? ByDescription,
            decimal? filterMin,
            decimal? filterMax,
            bool? SortPriceAsc,
            int pageNumber = 1)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            var filterEx = PredicateBuilder.New<Resource>(true);

            if (filterCategoryId != null)
                filterEx = filterEx.And(x => x.CategoryId == filterCategoryId);

            if (!string.IsNullOrWhiteSpace(ByName))
                filterEx = filterEx.And(x => x.Name.ToLower().Contains(ByName.ToLower()));

            if (!string.IsNullOrWhiteSpace(ByDescription))
                filterEx = filterEx.And(x => x.Description.ToLower().Contains(ByDescription.ToLower()));

            if (filterMin != null)
                filterEx = filterEx.And(x => x.PricePerHour >= filterMin.Value);
            if (filterMax != null)
                filterEx = filterEx.And(x => x.PricePerHour <= filterMax.Value);

            var query = repo.GetAllAsync(filtering: filterEx, includes: nameof(Resource.Category));

            var items = await query;


            if (SortPriceAsc != null)
            {
                items = SortPriceAsc.Value
                    ? items.OrderBy(x => x.PricePerHour).ToList()
                    : items.OrderByDescending(x => x.PricePerHour).ToList();
            }

            int pageSize = 5;
            items = items.Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();

            return mapper.Map<IList<ResourceGetDTO>>(items);
        }

        public async Task SeedResources()
        {
            ctx.Resources.RemoveRange(ctx.Resources);
            await ctx.SaveChangesAsync();

            var resources = new List<Resource>
    {
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Готель Київ",
            Description = "Комфортний готель у центрі міста.",
            PricePerHour = 1200,
            ImageUrl = "https://example.com/images/hotel1.jpg",
            PriceByHour = 1200,
            IsActive = true,
            CategoryId = Guid.Parse("447114B3-8D6C-428B-C506-08DE098055E7"),
            CategorySlug = CategorySlug.Hotels
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Апартаменти Skyline",
            Description = "Розкішні апартаменти з видом на місто.",
            PricePerHour = 1100,
            ImageUrl = "https://example.com/images/hotel2.jpg",
            PriceByHour = 1100,
            IsActive = true,
            CategoryId = Guid.Parse("447114B3-8D6C-428B-C506-08DE098055E7"),
            CategorySlug = CategorySlug.Hotels
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Кафе Aroma",
            Description = "Затишне кафе з авторською кухнею.",
            PricePerHour = 300,
            ImageUrl = "https://example.com/images/cafe1.jpg",
            PriceByHour = 300,
            IsActive = true,
            CategoryId = Guid.Parse("56930FFD-26D0-4B55-C507-08DE098055E7"),
            CategorySlug = CategorySlug.Restaurants
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Ресторан Ocean",
            Description = "Морська кухня від шеф-кухаря.",
            PricePerHour = 700,
            ImageUrl = "https://example.com/images/restaurant2.jpg",
            PriceByHour = 700,
            IsActive = true,
            CategoryId = Guid.Parse("56930FFD-26D0-4B55-C507-08DE098055E7"),
            CategorySlug = CategorySlug.Restaurants
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Футбольне поле",
            Description = "Професійне поле зі штучним покриттям.",
            PricePerHour = 500,
            ImageUrl = "https://example.com/images/field1.jpg",
            PriceByHour = 500,
            IsActive = true,
            CategoryId = Guid.Parse("7D2C4BC7-B88F-4897-C508-08DE098055E7"),
            CategorySlug = CategorySlug.Sports
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Баскетбольний зал",
            Description = "Зал для гри в баскетбол із сучасним покриттям.",
            PricePerHour = 400,
            ImageUrl = "https://example.com/images/sports2.jpg",
            PriceByHour = 400,
            IsActive = true,
            CategoryId = Guid.Parse("7D2C4BC7-B88F-4897-C508-08DE098055E7"),
            CategorySlug = CategorySlug.Sports
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Салон Lux Beauty",
            Description = "Сучасний салон краси для жінок і чоловіків.",
            PricePerHour = 350,
            ImageUrl = "https://example.com/images/beauty1.jpg",
            PriceByHour = 350,
            IsActive = true,
            CategoryId = Guid.Parse("2F9C4E14-F2BB-404D-C509-08DE098055E7"),
            CategorySlug = CategorySlug.Beauty
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Barber Shop",
            Description = "Чоловіча перукарня у стилі лофт.",
            PricePerHour = 250,
            ImageUrl = "https://example.com/images/barber.jpg",
            PriceByHour = 250,
            IsActive = true,
            CategoryId = Guid.Parse("2F9C4E14-F2BB-404D-C509-08DE098055E7"),
            CategorySlug = CategorySlug.Beauty
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Медичний центр Vita",
            Description = "Приватна клініка з сучасним обладнанням.",
            PricePerHour = 900,
            ImageUrl = "https://example.com/images/medical1.jpg",
            PriceByHour = 900,
            IsActive = true,
            CategoryId = Guid.Parse("8D283918-CBAD-4775-C50A-08DE098055E7"),
            CategorySlug = CategorySlug.Medical
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Стоматологічна клініка Smile",
            Description = "Ультрасучасна стоматологія.",
            PricePerHour = 1000,
            ImageUrl = "https://example.com/images/dentist.jpg",
            PriceByHour = 1000,
            IsActive = true,
            CategoryId = Guid.Parse("8D283918-CBAD-4775-C50A-08DE098055E7"),
            CategorySlug = CategorySlug.Medical
        }
        // можна додати решту ресурсів аналогічно
    };

            await ctx.Resources.AddRangeAsync(resources);
            await ctx.SaveChangesAsync();
        }



    }
}
