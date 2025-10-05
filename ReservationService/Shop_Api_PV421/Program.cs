using BusinessLogic.Configurations;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ReservationService;


var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("No Connection String found.");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAutoMapper(cfg => { }, typeof(MapperProfile));

builder.Services.AddDbContext<ReservationServiceDbContext>(options =>
    options.UseSqlServer(connStr));

builder.Services.AddScoped<IResourcesService, ResourcesService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
