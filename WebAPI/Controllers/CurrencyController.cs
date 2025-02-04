using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/currencies")]
[ApiController]
public class CurrencyController(ICurrenciesService currenciesService) : ControllerBase
{
    private readonly ICurrenciesService _currenciesService = currenciesService;

    [HttpPost]
    public async Task<IActionResult> Create(CurrencyRegForm form)
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

        var result = await _currenciesService.CreateCurrency(form);
        return result != null ? Ok(result) : Conflict();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search = null)
    {
        var result = await _currenciesService.GetCurrencies(search);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _currenciesService.GetCurrencyId(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> Update(Currencies currencies)
    {

        var existingCurrency = await _currenciesService.GetCurrencyId(currencies.Id);
        if (existingCurrency == null)
        {
            return NotFound();
        }

        var result = await _currenciesService.UpdateCurrency(currencies);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingCurrency = await _currenciesService.GetCurrencyId(id);
        if (existingCurrency == null)
        {
            return NotFound();
        }

        var result = await _currenciesService.DeleteCurrency(existingCurrency.Id);
        return result ? Ok() : BadRequest();

    }

}