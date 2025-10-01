using AutoMapper;
using BusinessLogic.DTOs.CategoryDTO;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReservationService.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ReservationServiceDbContext ctx;
        private readonly IMapper mapper;
        public CategoryController(ReservationServiceDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var items = ctx.Categories.ToList();

            return Ok(mapper.Map<IEnumerable<CategoryDTO>>(items));
        }
        [HttpGet]
        public IActionResult Get([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id can not be empty!");

            var item = ctx.Categories.Find(id);

            if (item == null)
                return NotFound("Category not found!");

            return Ok(mapper.Map<CategoryDTO>(item));
        }
        [HttpPost]
        public IActionResult Create([FromBody] CategoryCreateDTO category)
        {
            if (category == null)
                return BadRequest("Category is null!");
            var entity = mapper.Map<Category>(category);
            ctx.Categories.Add(entity);
            ctx.SaveChanges();

            return Ok(mapper.Map<CategoryDTO>(entity));
        }
        [HttpPut]
        public IActionResult Edit([FromBody] CategoryDTO category)
        {
            if (category == null)
                return BadRequest("Category is null!");

            var entity = ctx.Categories.Find(category.Id);
            if (entity == null)
                return NotFound("Category not found!");

            entity.Name = category.Name;

            ctx.Categories.Update(entity);
            ctx.SaveChanges();

            return Ok(mapper.Map<CategoryDTO>(entity));
        }
        [HttpDelete]
        public IActionResult Delete([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id can not be empty!");

            var entity = ctx.Categories.Find(id);
            if (entity == null)
                return NotFound("Category not found!");

            ctx.Categories.Remove(entity);
            ctx.SaveChanges();

            return Ok("Category deleted");
        }
        [HttpDelete("all")]
        public IActionResult DeleteAll()
        {
            ctx.Categories.RemoveRange(ctx.Categories);
            ctx.SaveChanges();

            return Ok();
        }
        [HttpPost("seed")]
        public IActionResult SeedCategories()
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

            return Ok("Categories seeded successfully!");
        }
    }
}
