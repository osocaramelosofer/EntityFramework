using API.Data;
using API.Models;
using API.Repository.Interfaces;
using API.Repository.Classes;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSql"));
});

// Support for .Net Identity
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();

// Add the repositories
builder.Services.AddScoped<IMachineStatusCatalog, API.Repository.Classes.MachineStatusCatalog>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AppMapper));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CORS
builder.Services.AddCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();
// Protect access
app.UseAuthentication();
app.MapControllers();

app.Run();
