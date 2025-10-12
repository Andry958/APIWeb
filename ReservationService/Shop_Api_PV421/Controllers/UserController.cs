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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
        {
            var res = await userServices.Login(userLoginModel);
            return Ok(res);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await userServices.Logout();
            return Ok("Ви успішно вийшли з акаунта");
        }
        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromQuery] string email)
        {
            var res = await userServices.ForgetPssword(email);
            return Ok(res);
        }
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email, [FromQuery] string token, [FromQuery] string newPassword)
        {
            await userServices.ResetPassword(email, token, newPassword);
            return Ok("Пароль успішно змінено");
        }

    }
}
