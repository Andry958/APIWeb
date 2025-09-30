using AutoMapper;
using BusinessLogic.DTOs.ResourceDTO;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop_Api_PV421.Controllers
{
    [Route("api/main")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ReservationServiceDbContext ctx;
        private readonly IMapper mapper;
        public MainController(ReservationServiceDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var items = ctx.Resources.ToList();

            return Ok(mapper.Map<IEnumerable<ResourceGetDTO>>(items));
        }
        
        [HttpGet]
        public IActionResult Get([FromQuery]Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id can not be empty!");

            var item = ctx.Resources.Find(id);

            if (item == null)
                return NotFound("Resource not found!");

            return Ok(mapper.Map<IEnumerable<ResourceGetDTO>>(item));

        }
        [HttpPost]
        public IActionResult Create([FromBody] ResourceCreateDTO resource)
        {
            if (resource == null)
                return BadRequest("Resource is null!");

            var entity = mapper.Map<Resource>(resource); 
            ctx.Resources.Add(entity);                    
            ctx.SaveChanges();

            return Ok(mapper.Map<ResourceGetDTO>(entity));                           
        }
        [HttpPut]
        public IActionResult Edit([FromBody] ResourceEditDTO resource)
        {
            if (resource == null)
                return BadRequest("Resource is null!");

            var existingResource = ctx.Resources.Find(resource.Id);
            if (existingResource == null)
                return NotFound("Resource not found!");

            var entity = mapper.Map(resource, existingResource);

            ctx.Resources.Add(entity);
            ctx.SaveChanges();

            return Ok(existingResource);
        }
        [HttpDelete]
        public IActionResult Delete([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id can not be empty!");

            var resource = ctx.Resources.Find(id);
            if (resource == null)
                return NotFound("Resource not found!");

            ctx.Resources.Remove(resource);
            ctx.SaveChanges();

            return Ok();
        }
    }
}
