using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/services")]
[ApiController]
public class ServiceController(IServicesService servicesService) : ControllerBase
{
    private readonly IServicesService _servicesService = servicesService;

    [HttpPost]
    public async Task<IActionResult> Create(ServiceRegForm form)
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

        var result = await _servicesService.CreateService(form);
        return result != null ? Ok(result) : Conflict();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search = null)
    {
        var result = await _servicesService.GetServices(search);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _servicesService.GetServiceId(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> Update(Service service)
    {

        var existingService = await _servicesService.GetServiceId(service.Id);
        if (existingService == null)
        {
            return NotFound();
        }

        var result = await _servicesService.UpdateService(service);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingService = await _servicesService.GetServiceId(id);
        if (existingService == null)
        {
            return NotFound();
        }

        var result = await _servicesService.DeleteService(existingService.Id);
        return result ? Ok() : BadRequest();

    }

}