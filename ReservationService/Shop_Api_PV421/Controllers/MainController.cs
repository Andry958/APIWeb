using AutoMapper;
using BusinessLogic.DTOs.ResourceDTO;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Shop_Api_PV421.Controllers
{
    [Route("api/main")]
    [ApiController]
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
        public IActionResult GetAll(Guid? filterCategoryId, string? ByName, string? ByDescription, decimal? filterMin, decimal? filterMax, bool? SortPriceAsc)
        {
            var items = resourcesService.GetAll(filterCategoryId, ByName, ByDescription, filterMin, filterMax, SortPriceAsc);
            return Ok(items);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var item = resourcesService.Get(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ResourceCreateDTO resource)
        {
            if (resource == null)
                return BadRequest();

            var created = resourcesService.Create(resource);
            return Ok(created);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] ResourceEditDTO resource)
        {
            if (resource == null)
                return BadRequest();

            if (resource.Id == Guid.Empty)
                return BadRequest();

            var existingResource = resourcesService.Get(resource.Id);
            if (existingResource == null)
                return NotFound();

            resourcesService.Edit(resource);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var resource = resourcesService.Get(id);
            if (resource == null)
                return NotFound();

            resourcesService.Delete(id);
            return Ok();
        }
        [HttpDelete("all")]
        public IActionResult DeleteAll()
        {
            resourcesService.DeleteAll();
            return Ok();
        }
        [HttpPost("seed-resources")]
        public IActionResult SeedResources()
        {
            
            resourcesService.SeedResources();
            return Ok("Resources seeded successfully!");
        }

    }
}
