using BusinessLogic.DTOs.ResourceDTO;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shop_Api_PV421.Controllers
{
    [Route("api/main")]
    [ApiController]
    //[Authorize]

    public class MainController : ControllerBase
    {
        private readonly IResourcesService resourcesService;
        private readonly ReservationServiceDbContext ctx;

        public MainController(IResourcesService resourcesService, ReservationServiceDbContext ctx)
        {
            this.resourcesService = resourcesService;
            this.ctx = ctx;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(Guid? filterCategoryId, string? ByName, string? ByDescription, decimal? filterMin, decimal? filterMax, bool? SortPriceAsc, int pageNumber = 1)
        {
            var items = await resourcesService.GetAll(filterCategoryId, ByName, ByDescription, filterMin, filterMax, SortPriceAsc, pageNumber);
            return Ok(items);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var item = await resourcesService.Get(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] ResourceCreateDTO resource)
        {

            if (resource == null)
                return BadRequest();

            var created = await resourcesService.Create(resource);
            return Ok(created);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> Edit([FromBody] ResourceEditDTO resource)
        {
            if (resource == null)
                return BadRequest();

            if (resource.Id == Guid.Empty)
                return BadRequest();

            var existingResource = await resourcesService.Get(resource.Id);
            if (existingResource == null)
                return NotFound();

            await resourcesService.Edit(resource);
            return Ok();
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var resource = await resourcesService.Get(id);
            if (resource == null)
                return NotFound();

            await resourcesService.Delete(id);
            return Ok();
        }
        [HttpDelete("all")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> DeleteAll()
        {
            await resourcesService.DeleteAll();
            return Ok();
        }
        [HttpPost("seed-resources")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> SeedResources()
        {
            await resourcesService.SeedResources();
            return Ok("Resources seeded successfully!");
        }

    }
}
