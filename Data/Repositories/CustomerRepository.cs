using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class CustomerRepository(AppDbContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{
    private readonly AppDbContext _context = context;



    public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Customers.Include(e => e.ContactPerson).ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Getting All Employees :: {ex.Message}");
            return null!;
        }

    }
}


