using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Data.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<StatusTypesEntity> StatusTypes { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ContactPersonEntity> ContactPerson { get; set; }
    public DbSet<CustomerContactPersonsEntity> CustomerContactPersons { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<RolesEntity> Roles { get; set; }
    public DbSet<UnitEntity> Units { get; set; }
    public DbSet<CurrencyEntity> Currencies { get; set; }


protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<EmployeeEntity>()
        .HasOne(e => e.Role)
        .WithMany(r => r.Employees)
        .HasForeignKey(e => e.RoleId)
        .OnDelete(DeleteBehavior.Cascade);
}
        
}
