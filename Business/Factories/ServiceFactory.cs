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
        ServiceDescription = form.ServiceDescription,
        Price = form.Price,
        StartupPrice = form.StartupPrice,
        UnitId = form.UnitId,
        CurrencyId = form.CurrencyId

    };

    public static Service Create(ServiceEntity entity) => new()
    {
        Id = entity.Id,
        ServiceName = entity.ServiceName,
        ServiceDescription = entity.ServiceDescription,
        StartupPrice = entity.StartupPrice,
        Price = entity.Price,
        UnitId = entity.UnitId,
        CurrencyId = entity.CurrencyId,
        Units = entity.Units != null ? UnitFactory.Create(entity.Units) : null,
        Currencies = entity.Currencies != null ? CurrencyFactory.Create(entity.Currencies) : null,

    };

    public static ServiceEntity Create(Service service, int Id) => new()
    {
        Id = service.Id,
        ServiceName = service.ServiceName,
        ServiceDescription = service.ServiceDescription,
        StartupPrice = service.StartupPrice,
        Price = service.Price,
        UnitId = service.UnitId,
        CurrencyId = service.CurrencyId,
   
    };
}
