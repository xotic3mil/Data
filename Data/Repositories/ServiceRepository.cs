using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class ServiceRepository(AppDbContext context) : BaseRepository<ServiceEntity>(context), IServiceRepository
{
     private readonly AppDbContext _context = context;


    public override async Task<IEnumerable<ServiceEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Services
                .Include(u => u.Units)
                .Include(c => c.Currencies)
                .ToListAsync();
            

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Getting All Employees :: {ex.Message}");
            return null!;
        }

    }

}
