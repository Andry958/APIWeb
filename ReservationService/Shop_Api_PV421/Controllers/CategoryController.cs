using AutoMapper;
using BusinessLogic.DTOs.CategoryDTO;
using BusinessLogic.Interfaces;
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
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("all")]
        public IActionResult GetAll(string? ByName, CategorySlug categorySlug)
        {
            var items = categoryService.GetAll(ByName, categorySlug);

            return Ok(items);
        }
        [HttpGet]
        public IActionResult Get([FromQuery] Guid id)
        {
            var item = categoryService.Get(id);

            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CategoryCreateDTO category)
        {
           var item = categoryService.Create(category);

            return Ok(item);
        }
        [HttpPut]
        public IActionResult Edit([FromBody] CategoryDTO category)
        {
            var item = categoryService.Edit(category);
            return Ok(item);
        }
        [HttpDelete]
        public IActionResult Delete([FromQuery] Guid id)
        {
            categoryService.Delete(id);

            return Ok("Category deleted");
        }
        [HttpDelete("all")]
        public IActionResult DeleteAll()
        {
            categoryService.DeleteAll();

            return Ok();
        }
        [HttpPost("seed")]
        public IActionResult SeedCategories()
        {
           categoryService.SeedCategories();

            return Ok("Categories seeded successfully!");
        }
    }
}
