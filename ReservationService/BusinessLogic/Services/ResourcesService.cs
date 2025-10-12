using AutoMapper;
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
using BusinessLogic.DTOs.ResourceDTO;
using BusinessLogic.Helpers;

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

            await ctx.Resources.AddAsync(entity);
            await ctx.SaveChangesAsync();

            // Повертаємо DTO
            return mapper.Map<ResourceGetDTO>(entity);
        }
        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty) return;

            var item = ctx.Resources.Find(id);
            if (item == null) return;

            ctx.Resources.Remove(item);
            await ctx.SaveChangesAsync();

        }

        public async Task DeleteAll()
        {
            ctx.Resources.RemoveRange(ctx.Resources);
            await ctx.SaveChangesAsync();
        }

        public async Task Edit(ResourceEditDTO model)
        {
            var existingResource = await ctx.Resources.FindAsync(model.Id);
            if (existingResource == null) return; 

            mapper.Map(model, existingResource);
            await ctx.SaveChangesAsync();
        }

        public async Task<ResourceGetDTO?> Get(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var item = await ctx.Resources.FindAsync(id);
            if (item == null)
                return null;

            return mapper.Map<ResourceGetDTO>(item);
        }

        public async Task<IList<ResourceGetDTO>> GetAll(Guid? filterCategoryId, string? ByName, string? ByDescription, decimal? filterMin, decimal? filterMax, bool? SortPriceAsc, int pageNumber = 1)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            IQueryable<Resource> query = ctx.Resources.Include(x => x.Category);

            if (filterCategoryId != null)
                query = query.Where(x => x.CategoryId == filterCategoryId);

            if (ByName != null)
                query = query.Where(x => x.Name.ToLower().Contains(ByName.ToLower()));

            if (ByDescription != null)
                query = query.Where(x => x.Description.ToLower().Contains(ByDescription.ToLower()));

            if (filterMin != null && filterMax != null)
                query = query.Where(x => x.PricePerHour >= filterMin && x.PricePerHour <= filterMax);

            if (SortPriceAsc != null)
                query = SortPriceAsc.Value ?
                    query.OrderBy(x => x.PricePerHour)
                        :
                    query = query.OrderByDescending(x => x.PricePerHour);


            var items = await PagedList<Resource>.CreateAsync(query, pageNumber, 5);

            return mapper.Map<IList<ResourceGetDTO>>(items);
        }

        public async Task SeedResources()
        {
            // Очищаємо старі ресурси
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
