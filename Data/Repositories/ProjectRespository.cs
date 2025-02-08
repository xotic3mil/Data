using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProjectRespository(AppDbContext context) : BaseRepository<ProjectEntity>(context), IProjectRespository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<ProjectEntity>> GetProjectsWithDetails()
        {
            var projects = await _context.Projects
            .Include(s => s.Status)
            .Include(c => c.Customer)
            .ThenInclude(cp => cp.ContactPerson)
            .Include(e => e.Employee)
            .ThenInclude(er => er.Role)
            .Include(b => b.Service)
            .ToListAsync();

            return projects;
        }

        public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Projects
                    .Include(s => s.Status)
                    .Include(c => c.Customer)
                    .ThenInclude(cp => cp.ContactPerson)
                    .Include(e => e.Employee)
                    .ThenInclude(er => er.Role)
                    .Include(b => b.Service)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error Getting All Projects :: {ex.Message}");
                return null!;
            }

        }


    }

}

