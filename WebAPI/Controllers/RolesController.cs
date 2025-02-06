using Business.Interfaces;
using Business.Dtos;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpPost]
        public async Task<IActionResult> Create(RolesRegForm form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            var result = await _roleService.CreateRole(form);
            return result != null ? Ok(result) : Conflict();
        }

     
        [HttpGet("special")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetRoles();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _roleService.GetRoleById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRoles();
            return Ok(roles);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Roles role)
        {
            var existingRole = await _roleService.GetRoleById(role.Id);
            if (existingRole == null)
            {
                return NotFound();
            }

            var result = await _roleService.UpdateRole(role);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingRole = await _roleService.GetRoleById(id);
            if (existingRole == null)
            {
                return NotFound();
            }

            var result = await _roleService.DeleteRole(existingRole.Id);
            return result ? Ok() : BadRequest();
        }
    }
}