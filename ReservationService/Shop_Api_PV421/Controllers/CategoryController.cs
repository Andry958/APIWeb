using AutoMapper;
using BusinessLogic.DTOs.CategoryDTO;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Enum;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReservationService.Controllers
{
    [Route("api/category")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(string? ByName, CategorySlug categorySlug,int pageNumber)
        {
            var items = await categoryService.GetAll(ByName, categorySlug, pageNumber);

            return Ok(items);
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var item = await categoryService.Get(id);

            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO category)
        {
            var item =  await categoryService.Create(category);

            return Ok(item);
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] CategoryDTO category)
        {
            var item = await categoryService.Edit(category);
            return Ok(item);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await categoryService.Delete(id);

            return Ok("Category deleted");
        }
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll()
        {
            await categoryService.DeleteAll();

            return Ok();
        }
        [HttpPost("seed")]
        public async Task<IActionResult> SeedCategories()
        {
            await categoryService.SeedCategories();

            return Ok("Categories seeded successfully!");
        }
    }
}
