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
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Employee)
            .WithMany(e => e.Projects)
            .HasForeignKey(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Status)
            .WithMany(s => s.Projects)
            .HasForeignKey(p => p.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Service)
            .WithMany(s => s.Projects)
            .HasForeignKey(p => p.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CustomerEntity>()
            .HasOne(c => c.ContactPersons)
            .WithMany()
            .HasForeignKey(c => c.ContactPersonId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<CustomerContactPersonsEntity>()
            .HasKey(ccp => new { ccp.CustomerId, ccp.ContactPersonId });


        modelBuilder.Entity<CustomerContactPersonsEntity>()
            .HasOne(ccp => ccp.ContactPersons)
            .WithMany()
            .HasForeignKey(ccp => ccp.ContactPersonId);
    }
        
}
