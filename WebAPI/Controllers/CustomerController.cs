using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController(ICustomersService customerService) : ControllerBase
    {
        private readonly ICustomersService _customerService = customerService;

        [HttpPost]
        public async Task<IActionResult> Create(CustomerRegForm form)
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

            var result = await _customerService.CreateCustomer(form);
            return result != null ? Ok(result) : Conflict();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null)
        {
            var result = await _customerService.GetCustomers(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _customerService.GetCustomerId(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customers customers)
        {
            var existingCustomer = await _customerService.GetCustomerId(customers.Id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            var result = await _customerService.UpdateCustomer(customers);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingCustomer = await _customerService.GetCustomerId(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            var result = await _customerService.DeleteCustomer(existingCustomer.Id);
            return result ? Ok() : BadRequest();
        }
    }
}