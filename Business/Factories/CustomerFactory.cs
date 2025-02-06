using Business.Dtos;
using Business.Models;
using Data.Entities;


namespace Business.Factories;

public class CustomerFactory
{
    public static CustomerRegForm Create() => new();

    public static CustomerEntity Create(CustomerRegForm form) => new()
    {
        CompanyName = form.CompanyName,
        CustomerContactPersonId = form.CustomerContactPersonId,
    };

    public static Customers Create(CustomerEntity entity) => new()
    {
        Id = entity.Id,
        CompanyName = entity.CompanyName,
        // CustomerContactPersonId = entity.CustomerContactPersonId,
        CustomerContactPerson = entity.ContactPerson != null ? CustomerContactPersonFactory.Create(entity.ContactPerson) : null,
    };

    public static CustomerEntity Create(Customers customers) => new()
    {
        Id = customers.Id,
        CompanyName = customers.CompanyName,
        // CustomerContactPersonId = customers.CustomerContactPersonId,
    };
}
