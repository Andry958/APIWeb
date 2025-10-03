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
            var resources = new List<Resource>
    {
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Готель Київ",
            Description = "Комфортний готель у центрі міста.",
            PricePerHour = 1200,
            ImageUrl = "https://example.com/images/hotel1.jpg",
            CategoryId = Guid.Parse("141b39b1-5760-41bd-6f34-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Кафе Aroma",
            Description = "Затишне кафе з авторською кухнею.",
            PricePerHour = 300,
            ImageUrl = "https://example.com/images/cafe1.jpg",
            CategoryId = Guid.Parse("65e261d9-4d62-490c-6f35-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Футбольне поле",
            Description = "Професійне поле зі штучним покриттям.",
            PricePerHour = 500,
            ImageUrl = "https://example.com/images/field1.jpg",
            CategoryId = Guid.Parse("1d3580bb-10bc-4836-6f36-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Салон Lux Beauty",
            Description = "Сучасний салон краси для жінок і чоловіків.",
            PricePerHour = 350,
            ImageUrl = "https://example.com/images/beauty1.jpg",
            CategoryId = Guid.Parse("50272102-ccc4-4c9a-6f37-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Медичний центр Vita",
            Description = "Приватна клініка з сучасним обладнанням.",
            PricePerHour = 900,
            ImageUrl = "https://example.com/images/medical1.jpg",
            CategoryId = Guid.Parse("2f3a057e-9c40-488e-6f38-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Конференц-зал Orion",
            Description = "Ідеальне місце для бізнес-заходів.",
            PricePerHour = 1500,
            ImageUrl = "https://example.com/images/conference1.jpg",
            CategoryId = Guid.Parse("e4f67fd5-e586-4df1-6f39-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Коворкінг Freedom",
            Description = "Сучасний простір для роботи в центрі.",
            PricePerHour = 200,
            ImageUrl = "https://example.com/images/coworking1.jpg",
            CategoryId = Guid.Parse("985dbba0-7a5c-4ac8-6f3a-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Оренда Tesla Model 3",
            Description = "Електромобіль преміум класу для подорожей.",
            PricePerHour = 800,
            ImageUrl = "https://example.com/images/car1.jpg",
            CategoryId = Guid.Parse("2e420687-84f8-44ee-6f3b-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "СТО АвтоСервіс",
            Description = "Швидке та якісне обслуговування вашого авто.",
            PricePerHour = 450,
            ImageUrl = "https://example.com/images/service1.jpg",
            CategoryId = Guid.Parse("d4bd21a3-c55a-400b-6f3c-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Квест кімната 'Втеча'",
            Description = "Захоплюючий квест для компанії друзів.",
            PricePerHour = 600,
            ImageUrl = "https://example.com/images/quest1.jpg",
            CategoryId = Guid.Parse("9a268634-4dc6-4bb1-6f3d-08de02915a7a")
        },

        // Додаткові 10 ресурсів (вигадані приклади)
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Апартаменти Skyline",
            Description = "Розкішні апартаменти з видом на місто.",
            PricePerHour = 1100,
            ImageUrl = "https://example.com/images/hotel2.jpg",
            CategoryId = Guid.Parse("141b39b1-5760-41bd-6f34-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Ресторан Ocean",
            Description = "Морська кухня від шеф-кухаря.",
            PricePerHour = 700,
            ImageUrl = "https://example.com/images/restaurant2.jpg",
            CategoryId = Guid.Parse("65e261d9-4d62-490c-6f35-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Баскетбольний зал",
            Description = "Зал для гри в баскетбол із сучасним покриттям.",
            PricePerHour = 400,
            ImageUrl = "https://example.com/images/sports2.jpg",
            CategoryId = Guid.Parse("1d3580bb-10bc-4836-6f36-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Barber Shop",
            Description = "Чоловіча перукарня у стилі лофт.",
            PricePerHour = 250,
            ImageUrl = "https://example.com/images/barber.jpg",
            CategoryId = Guid.Parse("50272102-ccc4-4c9a-6f37-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Стоматологічна клініка Smile",
            Description = "Ультрасучасна стоматологія.",
            PricePerHour = 1000,
            ImageUrl = "https://example.com/images/dentist.jpg",
            CategoryId = Guid.Parse("2f3a057e-9c40-488e-6f38-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Конференц-зал SkyRoom",
            Description = "Ідеальне місце для презентацій.",
            PricePerHour = 1400,
            ImageUrl = "https://example.com/images/conference2.jpg",
            CategoryId = Guid.Parse("e4f67fd5-e586-4df1-6f39-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Коворкінг WorkZone",
            Description = "Тихе місце для продуктивної роботи.",
            PricePerHour = 250,
            ImageUrl = "https://example.com/images/coworking2.jpg",
            CategoryId = Guid.Parse("985dbba0-7a5c-4ac8-6f3a-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Оренда BMW X5",
            Description = "Позашляховик бізнес-класу.",
            PricePerHour = 1000,
            ImageUrl = "https://example.com/images/bmw.jpg",
            CategoryId = Guid.Parse("2e420687-84f8-44ee-6f3b-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "СТО TurboFix",
            Description = "Діагностика та ремонт усіх марок авто.",
            PricePerHour = 480,
            ImageUrl = "https://example.com/images/sto2.jpg",
            CategoryId = Guid.Parse("d4bd21a3-c55a-400b-6f3c-08de02915a7a")
        },
        new Resource
        {
            Id = Guid.NewGuid(),
            Name = "Батутний парк JumpCity",
            Description = "Розваги для дітей і дорослих.",
            PricePerHour = 550,
            ImageUrl = "https://example.com/images/jump.jpg",
            CategoryId = Guid.Parse("9a268634-4dc6-4bb1-6f3d-08de02915a7a")
        }
    };

            ctx.Resources.AddRange(resources);
            ctx.SaveChanges();

            return Ok("Resources seeded successfully!");
        }

    }
}
