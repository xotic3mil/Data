using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

internal class UnitRespository(AppDbContext context) : BaseRepository<UnitEntity>(context), IUnitEntity
{
    private readonly AppDbContext _context = context;
}
