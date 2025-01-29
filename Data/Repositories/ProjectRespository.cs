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
    public class ProjectRespository(AppDbContext context) : BaseRepository<ProjectEntity>(context), IProjectRespository
    {
        private readonly AppDbContext _context = context;
    }

}
