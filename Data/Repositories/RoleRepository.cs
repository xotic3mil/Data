using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class RoleRepository(AppDbContext context) : BaseRepository<RolesEntity>(context), IRoleRepository
{
    private readonly AppDbContext _context = context; 

}