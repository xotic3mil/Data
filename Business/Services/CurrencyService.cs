using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CurrencyService(ICurrencyRepository currencyRepository) : ICurrenciesService
    {
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;

        public async Task<Currencies> CreateCurrency(CurrencyRegForm form)
        {
            var currencyEntity = await _currencyRepository.GetAsync(x => x.Currency == form.Currency);

            if (currencyEntity != null)
            {
                return null!;
            }

            currencyEntity = CurrencyFactory.Create(form);

            try 
            {
                await _currencyRepository.BeginTransactionAsync();
                currencyEntity = await _currencyRepository.CreateAsync(currencyEntity);
                await _currencyRepository.SaveChangesAsync();
                await _currencyRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _currencyRepository.RollbackTransactionAsync();
                return null!;
            }
            
            return CurrencyFactory.Create(currencyEntity);

        }

        public async Task<bool> DeleteCurrency(int id)
        {
            try 
            {
                await _currencyRepository.BeginTransactionAsync();
                var result = await _currencyRepository.DeleteAsync(x => x.Id == id);
                await _currencyRepository.SaveChangesAsync();
                await _currencyRepository.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _currencyRepository.RollbackTransactionAsync();
                return false;
            }
        }

        public async Task<IEnumerable<Currencies>> GetCurrencies(string? search)
        {
            var currencies = await _currencyRepository.GetAllAsync();
            return currencies.Select(CurrencyFactory.Create);
        }

        public async Task<Currencies> GetCurrencyId(int id)
        {
            var currencyEntity = await _currencyRepository.GetAsync(x => x.Id == id);
            if (currencyEntity == null)
            {
               
                return null;
            }
            return CurrencyFactory.Create(currencyEntity);
        }

        public async Task<Currencies> UpdateCurrency(Currencies currencies)
        {
            var currencyEntity = CurrencyFactory.Create(currencies);
            try 
            {
                await _currencyRepository.BeginTransactionAsync();
                currencyEntity = await _currencyRepository.UpdateAsync(x => x.Id == currencies.Id, currencyEntity);
                await _currencyRepository.SaveChangesAsync();
                await _currencyRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _currencyRepository.RollbackTransactionAsync();
                return null!;
            }
            
            return CurrencyFactory.Create(currencyEntity);
        }
    }
}
