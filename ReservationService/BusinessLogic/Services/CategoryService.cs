using AutoMapper;
using BusinessLogic.DTOs.CategoryDTO;
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
        public CategoryDTO Create(CategoryCreateDTO category)
        {
            if (category == null)
                return null;
            var entity = mapper.Map<Category>(category);
            ctx.Categories.Add(entity);
            ctx.SaveChanges();

            return mapper.Map<CategoryDTO>(entity);
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                return;

            var entity = ctx.Categories.Find(id);
            if (entity == null)
                return;

            ctx.Categories.Remove(entity);
            ctx.SaveChanges();
        }

        public void DeleteAll()
        {
            ctx.Categories.RemoveRange(ctx.Categories);
            ctx.SaveChanges();
        }

        public CategoryDTO Edit(CategoryDTO category)
        {
            if (category == null)
                return null;

            var entity = ctx.Categories.Find(category.Id);
            if (entity == null)
                return null;

            entity.Name = category.Name;
            entity.Slug = category.Slug;

            ctx.Categories.Update(entity);
            ctx.SaveChanges();

            return mapper.Map<CategoryDTO>(entity);
        }
        public CategoryDTO? Get(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var item = ctx.Categories.Find(id);

            if (item == null)
                return null;

            return mapper.Map<CategoryDTO>(item);
        }

        public IList<CategoryDTO> GetAll(string? ByName, CategorySlug categorySlug)
        {
            IQueryable<Category> query = ctx.Categories;

            if (!string.IsNullOrEmpty(ByName))
                query = query.Where(c => c.Name.ToLower().Contains(ByName.ToLower()));
            if (categorySlug != 0)
                query = query.Where(c => c.Slug == categorySlug);


            var items = query.ToList();

            return mapper.Map<IList<CategoryDTO>>(items);      
        }

        public void SeedCategories()
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

            ctx.Categories.AddRange(categories);
            ctx.SaveChanges();
        }
    }
}
