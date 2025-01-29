using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ServiceRepository(AppDbContext context) : BaseRepository<ServiceEntity>(context), IServiceRepository
{
     private readonly AppDbContext _context = context; 
    
}
