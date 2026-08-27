using Microsoft.EntityFrameworkCore;
using SmartBarber.Application.Abstraction;
using SmartBarber.Application.Abstraction.Repositories;
using SmartBarber.Infrastructure.Persistence;
using SmartBarber.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SmartBarberDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SmartBarber")));

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
