using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Interfaces;
using TravelAgencyAPI.Models;
using TravelAgencyAPI.Models.Validators;
using TravelAgencyAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<TravelAgencyDbContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();  
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserValidator>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
