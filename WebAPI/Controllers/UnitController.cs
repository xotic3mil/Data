using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/units")]
[ApiController]
public class UnitController(IUnitService unitService) : ControllerBase
{
    private readonly IUnitService _unitService = unitService;

    [HttpPost]
    public async Task<IActionResult> Create(UnitRegForm form)
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

        var result = await _unitService.CreateUnit(form);
        return result != null ? Ok(result) : Conflict();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search = null)
    {
        var result = await _unitService.GetUnits(search);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _unitService.GetUnitId(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> Update(Units units)
    {

        var existingUnit = await _unitService.GetUnitId(units.Id);
        if (existingUnit == null)
        {
            return NotFound();
        }

        var result = await _unitService.UpdateUnit(units);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingUnit = await _unitService.GetUnitId(id);
        if (existingUnit == null)
        {
            return NotFound();
        }

        var result = await _unitService.DeleteUnit(existingUnit.Id);
        return result ? Ok() : BadRequest();

    }

}
