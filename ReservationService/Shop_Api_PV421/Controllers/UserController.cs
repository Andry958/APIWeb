using BusinessLogic.DTOs.Users;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ReservationService.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices userServices;

        public UserController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            await userServices.Register(model);
            return Ok("Успішно добавленно");
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> Getall()
        {
            var users = await userServices.GetAll();
            return Ok(users);
        }
    }
}
