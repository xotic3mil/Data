using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class CurrencyRepository(AppDbContext context) : BaseRepository<CurrencyEntity>(context), ICurrencyRepository
{
    private readonly AppDbContext _context = context; 
}
