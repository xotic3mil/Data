using Business.Models;
using Business.Dtos;
using Data.Entities;

namespace Business.Factories;

public static class EmployeeFactory
{
    public static EmployeeRegForm Create() => new();

    public static EmployeeEntity Create(EmployeeRegForm form) => new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email,
        Phone = form.Phone,
        RoleId = form.RoleId,
    };

    public static Employee Create(EmployeeEntity entity) => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Email = entity.Email,
        Phone = entity.Phone,
        RoleId = entity.RoleId,
        Roles = entity.Role != null ? RoleFactory.Create(entity.Role) : null,
    };

    public static EmployeeEntity Create(Employee employee, int roleId) => new()
    {
        Id = employee.Id,
        FirstName = employee.FirstName,
        LastName = employee.LastName,
        Email = employee.Email,
        Phone = employee.Phone,
        RoleId = employee.RoleId,
    };

}
