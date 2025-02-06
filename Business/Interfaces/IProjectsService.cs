using Business.Dtos;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IProjectsService
    {

        public Task<Projects> CreateProject(ProjectRegForm form);

        public Task<Projects> GetProjectById(int id);

        public Task<IEnumerable<Projects>> GetProjects(string? search);

        public Task<Projects> UpdateProject(Projects projects);

        public Task<bool> DeleteProject(int id);
    }
}
