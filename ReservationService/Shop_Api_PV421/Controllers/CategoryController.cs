using AutoMapper;
using BusinessLogic.DTOs.CategoryDTO;
using DataAccess.Data;
using DataAccess.Data.Entities;
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
    }
}
