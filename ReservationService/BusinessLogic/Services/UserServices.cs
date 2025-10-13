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
        public async Task<LoginResponse> Refresh(RefreshRequest model, string? ipAddress)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == model.RefreshToken));
            if (user == null)
                throw new HttpException("user is null", HttpStatusCode.NotFound);

            var token = await ctx.RefreshTokens.SingleAsync(x => x.Token == model.RefreshToken);

            if(!token.IsActive)
                throw new HttpException("Invalid refresh token", HttpStatusCode.Unauthorized);

            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;

            var newJwt = jwtService.GenerateToken(jwtService.GetClaims(user));
            var newRefreshJwt = jwtService.GenerateRefreshToken(ipAddress ?? "unknow");

            user.RefreshTokens.Add(newRefreshJwt);

            await ctx.SaveChangesAsync();

            return new LoginResponse
            {
                AccessToken = newJwt,
                RefreshToken = newRefreshJwt.Token
            };
        }
        public async Task<LoginResponse> Login(UserLoginModel model, string? ipAddress)
        {
            var user = await userManager.FindByNameAsync(model.Login);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Wrong login or password!", HttpStatusCode.BadRequest);

            var refreshToken = jwtService.GenerateRefreshToken(ipAddress ?? "unknown");
            user.RefreshTokens.Add(refreshToken);


            await ctx.SaveChangesAsync();
            //await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            return new()
            {
                AccessToken = jwtService.GenerateToken(jwtService.GetClaims(user)),
                RefreshToken = refreshToken.Token
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
        public async Task Delete(string id)
        {
            var user = await ctx.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new HttpException("Користувача не знайдено", HttpStatusCode.NotFound);

            await userManager.DeleteAsync(user);
            
        }
    }
}
