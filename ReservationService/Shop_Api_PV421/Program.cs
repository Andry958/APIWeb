using BusinessLogic.Configurations;
using DataAccess.Data;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;


var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("No Connection String found.");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAutoMapper(cfg => { }, typeof(MapperProfile));

builder.Services.AddDbContext<ReservationServiceDbContext>(options =>
    options.UseSqlServer(connStr));

builder.Services.AddScoped<IResourcesService, ResourcesService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
