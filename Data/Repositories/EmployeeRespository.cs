using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace Data.Repositories;

public class EmployeeRespository(AppDbContext context) : BaseRepository<EmployeeEntity>(context), IEmployeeRepository
{
    private readonly AppDbContext _context = context;


    public async Task<IEnumerable<EmployeeEntity>> GetEmployeesByRole(int roleId)
    {
        var employees = await _context.Employees
        .Include(e => e.RoleId)
        .ToListAsync();

        return employees;
    }

    public override async Task<IEnumerable<EmployeeEntity>> GetAllAsync()
    {
        try {
            return await _context.Employees.Include(e => e.Role).ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Getting All Employees :: {ex.Message}");
            return null!;
        }

    }
}