using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRespository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRespository>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<ICurrenciesService, CurrencyService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IRoleService, RoleService>();


var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.MapControllers();
app.Run();
