using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class StatusTypeFactory
{
    public static StatusTypeRegForm Create() => new();

    public static StatusTypesEntity Create(StatusTypeRegForm form) => new()
    {
        Status = form.Status
    };

    public static StatusTypes Create(StatusTypesEntity entity) => new()
    {
        Id = entity.Id,
        Status = entity.Status
    };
    public static StatusTypesEntity Create(StatusTypes status) => new()
    {
        Id = status.Id,
        Status = status.Status 
    };
}
