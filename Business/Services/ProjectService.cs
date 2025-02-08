using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProjectService(IProjectRespository projectRespository) : IProjectsService
    {

        private readonly IProjectRespository _projectRespository = projectRespository;

        public async Task<Projects> CreateProject(ProjectRegForm form)
        {
            var projectEntity = await _projectRespository.GetAsync(x => x.Name == form.Name);

            if (projectEntity != null)
            {
                return null!;
            }

            projectEntity = ProjectFactory.Create(form);
            projectEntity = await _projectRespository.CreateAsync(projectEntity);
            return ProjectFactory.Create(projectEntity);
        }

        public async Task<Projects> GetProjectById(int id)
        {
            var projectEntity = await _projectRespository.GetAsync(x => x.Id == id);
            return ProjectFactory.Create(projectEntity) ?? null!;
        }


        public async Task<IEnumerable<Projects>> GetProjects(string? search)
        {
            var projects = await _projectRespository.GetAllAsync();


            return projects.Select(ProjectFactory.Create);

        }

        public async Task<Projects> UpdateProject(Projects projects)
        {

            var projectEntity = ProjectFactory.Create(projects);
            projectEntity = await _projectRespository.UpdateAsync(x => x.Id == projects.Id, projectEntity);
            return ProjectFactory.Create(projectEntity);
        }

        public async Task<bool> DeleteProject(int id)
        {
            var result = await _projectRespository.DeleteAsync(x => x.Id == id);
            return result;
        }

    
    }
}
