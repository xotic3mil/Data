using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ServiceFactory
{
    public static ServiceRegForm Create() => new();

    public static ServiceEntity Create(ServiceRegForm form) => new()
    {
        ServiceName = form.ServiceName,
        Price = form.Price,
        UnitId = form.UnitId,
        CurrencyId = form.CurrencyId
    };

    public static Service Create(ServiceEntity entity) => new()
    {
        Id = entity.Id,
        ServiceName = entity.ServiceName,
        Price = entity.Price,
        UnitId = entity.UnitId,
        CurrencyId = entity.CurrencyId,
        Units = UnitFactory.Create(entity.Units),
        Currencies = CurrencyFactory.Create(entity.Currencies)

    };

    public static ServiceEntity Create(Service service, int Id) => new()
    {
        Id = service.Id,
        ServiceName = service.ServiceName,
        Price = service.Price,
        UnitId = service.UnitId,
        CurrencyId = service.CurrencyId,
   
    };
}
