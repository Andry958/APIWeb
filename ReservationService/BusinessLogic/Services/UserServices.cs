using AutoMapper;
using BusinessLogic.DTOs.Users;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UserServices : IUserServices
    {
        private readonly IMapper mapper;
        private readonly IJwtService jwtService;

        private readonly UserManager<User> userManager;
        private readonly ReservationServiceDbContext ctx;
        private readonly SignInManager<User> signInManager;
        public UserServices(UserManager<User> userManager, IMapper mapper, ReservationServiceDbContext ctx, SignInManager<User> signInManager, IJwtService jwtService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.ctx = ctx;
            this.signInManager = signInManager;
            this.jwtService = jwtService;
        }

        public async Task<LoginResponse> Login(UserLoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Login);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Wrong login or password!", HttpStatusCode.BadRequest);

            //await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            return new()
            {
                AccessToken = jwtService.GenerateToken(jwtService.GetClaims(user))
            };
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task Register(UserRegisterModel model)
        {
            var user = mapper.Map<User>(model);

            var res = await userManager.CreateAsync(user, model.Password);

            if (!res.Succeeded)
                throw new HttpException(res.Errors?.FirstOrDefault()?.Description ?? "Error", HttpStatusCode.BadRequest);
        }
        public async Task<List<UserGetModel>> GetAll()
        {
            var users = await ctx.Users.ToListAsync();
            return mapper.Map<List<UserGetModel>>(users);
        }
        public async Task<string> ForgetPssword(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);

            if (user == null)
                throw new HttpException("Користувача з такім емейлом немає", HttpStatusCode.BadRequest);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            
            return token;
        }
        public async Task ResetPassword(string email, string token, string newPassword)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                throw new HttpException("Користувача з такім емейлом немає", HttpStatusCode.BadRequest);
            
            await userManager.ResetPasswordAsync(user, token, newPassword);
        }

    }
}
