using AutoMapper;
using BusinessLogic.DTOs.CategoryDTO;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ReservationServiceDbContext ctx;
        private readonly IMapper mapper;
        public CategoryService(ReservationServiceDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }
        public async Task<CategoryDTO> Create(CategoryCreateDTO category)
        {
            if (category == null)
                return null;
            var entity = mapper.Map<Category>(category);
            await ctx.Categories.AddAsync(entity);
            await ctx.SaveChangesAsync();

            return mapper.Map<CategoryDTO>(entity);
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
                return;

            var entity = ctx.Categories.Find(id);
            if (entity == null)
                return;

            ctx.Categories.Remove(entity);
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteAll()
        {
            ctx.Categories.RemoveRange(ctx.Categories);
            await ctx.SaveChangesAsync();
        }

        public async Task<CategoryDTO> Edit(CategoryDTO category)
        {
            if (category == null)
                return null;

            var entity = await ctx.Categories.FindAsync(category.Id);
            if (entity == null)
                return null;

            entity.Name = category.Name;
            entity.Slug = category.Slug;

            ctx.Categories.Update(entity);
            await ctx.SaveChangesAsync();

            return mapper.Map<CategoryDTO>(entity);
        }
        public async Task<CategoryDTO?> Get(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var item = await ctx.Categories.FindAsync(id);

            if (item == null)
                return null;

            return mapper.Map<CategoryDTO>(item);
        }

        public async Task<IList<CategoryDTO>> GetAll(string? ByName, CategorySlug categorySlug, int pageNumber = 1)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            IQueryable<Category> query = ctx.Categories;

            if (!string.IsNullOrEmpty(ByName))
                query = query.Where(c => c.Name.ToLower().Contains(ByName.ToLower()));

            if (categorySlug != 0)
                query = query.Where(c => c.Slug == categorySlug);

            var items = await PagedList<Category>.CreateAsync(query, pageNumber, 5);
            return mapper.Map<IList<CategoryDTO>>(items);      
        }

        public async Task SeedCategories()
        {
            var categories = new List<Category>
            {
                new Category { Name = "Готелі та апартаменти", Slug = CategorySlug.Hotels },
                new Category { Name = "Ресторани та кафе", Slug = CategorySlug.Restaurants },
                new Category { Name = "Спортивні майданчики", Slug = CategorySlug.Sports },
                new Category { Name = "Салони краси", Slug = CategorySlug.Beauty },
                new Category { Name = "Медичні послуги", Slug = CategorySlug.Medical },
                new Category { Name = "Конференц-зали", Slug = CategorySlug.Conference },
                new Category { Name = "Коворкінги", Slug = CategorySlug.Coworking },
                new Category { Name = "Оренда авто", Slug = CategorySlug.CarRental },
                new Category { Name = "Майстерні та СТО", Slug = CategorySlug.AutoService },
                new Category { Name = "Розваги", Slug = CategorySlug.Entertainment }
            };

            await ctx.Categories.AddRangeAsync(categories);
            await ctx.SaveChangesAsync();
        }
    }
}
