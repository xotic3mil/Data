using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class UnitRespository(AppDbContext context) : BaseRepository<UnitEntity>(context), IUnitRepository
{
    private readonly AppDbContext _context = context;
}
