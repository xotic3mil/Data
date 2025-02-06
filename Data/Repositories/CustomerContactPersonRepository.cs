using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CustomerContactPersonRepository(AppDbContext context) : BaseRepository<CustomerContactPersonEntity>(context), ICustomerContactPersonRepository
    {
        private readonly AppDbContext _context = context;
    }
}
