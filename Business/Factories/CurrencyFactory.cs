using Business.Dtos;
using Business.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Factories
{
    public class CurrencyFactory
    {
        public static CurrencyRegForm Create() => new();

        public static CurrencyEntity Create(CurrencyRegForm form) => new()
        {
            Currency = form.Currency
        };

        public static Currencies Create(CurrencyEntity entity) => new()
        {
            Id = entity.Id,
            Currency = entity.Currency
        };

        public static CurrencyEntity Create(Currencies currency) => new()
        {
            Id = currency.Id,
            Currency = currency.Currency
        };
    }
}
