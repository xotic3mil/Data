using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController(IStatusTypeService statusTypeService) : ControllerBase
    {
        private readonly IStatusTypeService _statusTypeService = statusTypeService;

        [HttpPost]
        public async Task<IActionResult> Create(StatusTypeRegForm form)
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

            var result = await _statusTypeService.CreateStatus(form);
            return result != null ? Ok(result) : Conflict();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null)
        {
            var result = await _statusTypeService.GetStatus(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _statusTypeService.GetStatusById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update(StatusTypes statusTypes)
        {

            var existingStatusType = await _statusTypeService.GetStatusById(statusTypes.Id);
            if (existingStatusType == null)
            {
                return NotFound();
            }

            var result = await _statusTypeService.UpdateStatus(statusTypes);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingStatusType = await _statusTypeService.GetStatusById(id);
            if (existingStatusType == null)
            {
                return NotFound();
            }

            var result = await _statusTypeService.DeleteStatus(existingStatusType.Id);
            return result ? Ok() : BadRequest();

        }

    }
}
