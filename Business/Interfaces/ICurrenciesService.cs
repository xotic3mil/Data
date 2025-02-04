using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface ICurrenciesService
{

    public Task<Currencies> CreateCurrency(CurrencyRegForm form);

    public Task<Currencies> GetCurrencyId(int id);

    public Task<IEnumerable<Currencies>> GetCurrencies(string? search);

    public Task<Currencies> UpdateCurrency(Currencies currencies);

    public Task<bool> DeleteCurrency(int id);
}
