using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

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
        try 
        {
            await _projectRespository.BeginTransactionAsync();
            projectEntity = await _projectRespository.CreateAsync(projectEntity);
            await _projectRespository.SaveChangesAsync();
            await _projectRespository.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _projectRespository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return null!;
        }
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

    public async Task<IEnumerable<Projects>> SearchProjectsAsync(string? search)
    {
        var projects = await _projectRespository.SearchProjectsAsync(search);
        return projects.Select(ProjectFactory.Create);
    }

    public async Task<Projects> UpdateProject(Projects projects)
    {

        var projectEntity = ProjectFactory.Create(projects);
        try 
        {
            await _projectRespository.BeginTransactionAsync();
            projectEntity = await _projectRespository.UpdateAsync(x => x.Id == projects.Id, projectEntity);
            await _projectRespository.SaveChangesAsync();
            await _projectRespository.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _projectRespository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return null!;
        }
        return ProjectFactory.Create(projectEntity);
    }

    public async Task<bool> DeleteProject(int id)
    {

        try 
        {
            await _projectRespository.BeginTransactionAsync();
            var result = await _projectRespository.DeleteAsync(x => x.Id == id);
            await _projectRespository.SaveChangesAsync();
            await _projectRespository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            await _projectRespository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return false;
        }

    }


}
