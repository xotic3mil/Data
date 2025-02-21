using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/employees")]
[ApiController]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeRegForm form)
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

        var result = await _employeeService.CreateEmployee(form);
        return result != null ? Ok(result) : Conflict();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search = null)
    {
        var result = await _employeeService.GetEmployees(search);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAll([FromQuery] string? search = null)
    {

        var result = await _employeeService.SearchEmployeesAsync(search);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _employeeService.GetEmployeeId(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> Update(Employee employee)
    {

        var existingEmployee = await _employeeService.GetEmployeeId(employee.Id);
        if (existingEmployee == null)
        {
            return NotFound();
        }

        var result = await _employeeService.UpdateEmployee(employee);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingEmployee = await _employeeService.GetEmployeeId(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }

        var result = await _employeeService.DeleteEmployee(existingEmployee.Id);
        return result ? Ok() : BadRequest();

    }

}
