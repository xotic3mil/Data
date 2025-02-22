using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebAPI.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController(IProjectsService projectsService) : ControllerBase
    {
        private readonly IProjectsService _projectsService = projectsService;

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRegForm form)
        {
            Debug.WriteLine(form);
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            var result = await _projectsService.CreateProject(form);
            return result != null ? Ok(result) : Conflict();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null)
        {
            var result = await _projectsService.GetProjects(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _projectsService.GetProjectById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAll([FromQuery] string? search = null)
        {
            var result = await _projectsService.SearchProjectsAsync(search);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Projects projects)
        {

            var existingProject = await _projectsService.GetProjectById(projects.Id);
            if (existingProject == null)
            {
                return NotFound();
            }

            var result = await _projectsService.UpdateProject(projects);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProject = await _projectsService.GetProjectById(id);
            if (existingProject == null)
            {
                return NotFound();
            }

            var result = await _projectsService.DeleteProject(existingProject.Id);
            return result ? Ok() : BadRequest();

        }

    }
}
