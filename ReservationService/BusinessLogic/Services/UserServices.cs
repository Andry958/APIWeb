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
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly ReservationServiceDbContext ctx;
        public UserServices(UserManager<User> userManager, IMapper mapper, ReservationServiceDbContext ctx)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.ctx = ctx;
        }

        public Task Login(UserLoginModel model)
        {
            throw new NotImplementedException();
        }

        public Task Logout(LogoutModel model)
        {
            throw new NotImplementedException();
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

    }
}
