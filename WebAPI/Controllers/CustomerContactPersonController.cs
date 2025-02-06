using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/contactperson")]
    [ApiController]
    public class CustomerContactPersonController(ICustomerContactPersonService customerContactPersonService) : ControllerBase
    {
        private readonly ICustomerContactPersonService _customerContactPersonService = customerContactPersonService;

        [HttpPost]
        public async Task<IActionResult> Create(CustomerContactPersonRegForm form)
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

            var result = await _customerContactPersonService.CreateCustomerContactPerson(form);
            return result != null ? Ok(result) : Conflict();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null)
        {
            var result = await _customerContactPersonService.GetCustomerContactPerson(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _customerContactPersonService.GetCustomerContactPersonId(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerContactPerson customerContactPerson)
        {

            var existingCustomerContactPerson = await _customerContactPersonService.GetCustomerContactPersonId(customerContactPerson.Id);
            if (existingCustomerContactPerson == null)
            {
                return NotFound();
            }

            var result = await _customerContactPersonService.UpdateCustomerContactPerson(customerContactPerson);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingCustomerContactPerson = await _customerContactPersonService.GetCustomerContactPersonId(id);
            if (existingCustomerContactPerson == null)
            {
                return NotFound();
            }

            var result = await _customerContactPersonService.DeleteCustomerContactPerson(existingCustomerContactPerson.Id);
            return result ? Ok() : BadRequest();

        }
    }
}
