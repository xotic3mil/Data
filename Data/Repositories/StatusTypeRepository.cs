using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class StatusTypeRepository(AppDbContext context) : BaseRepository<StatusTypesEntity>(context), IStatusTypeRepository
{
     private readonly AppDbContext _context = context;


}
