using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Business.Dtos.Validators;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<ValidatorConfiguration>();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeRegFormValidator>();

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

// Register Repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRespository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRespository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerContactPersonRepository, CustomerContactPersonRepository>();
builder.Services.AddScoped<IProjectRespository, ProjectRespository>();
builder.Services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();

// Register Services
builder.Services.AddScoped<IStatusTypeService, StatusTypeService>();
builder.Services.AddScoped<IProjectsService, ProjectService>();
builder.Services.AddScoped<ICustomerContactPersonService, CustomerContactPersonService>();
builder.Services.AddScoped<ICustomersService, CustomerService>();
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
