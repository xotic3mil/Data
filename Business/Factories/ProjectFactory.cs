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
    public class ProjectFactory
    {
        public static ProjectRegForm Create() => new();

        public static ProjectEntity Create(ProjectRegForm form) => new()
        {
            Name = form.Name,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            ServiceId = form.ServiceId,
            CustomerId = form.CustomerId,
            EmployeeId = form.EmployeeId,
            StatusId = form.StatusId,
        };

        public static Projects Create(ProjectEntity entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            ServiceId = entity.ServiceId,
            Service = entity.Service != null ? ServiceFactory.Create(entity.Service) : null,
            Customers = entity.Customer != null ? CustomerFactory.Create(entity.Customer) : null,
            CustomerId = entity.CustomerId,
            Employee = entity.Employee != null ? EmployeeFactory.Create(entity.Employee) : null,
            EmployeeId = entity.EmployeeId,
            Status = entity.Status != null ? StatusTypeFactory.Create(entity.Status) : null,
            StatusId = entity.StatusId,
        };

        public static ProjectEntity Create(Projects projects) => new()
        {
            Id = projects.Id,
            Name = projects.Name,
            Description = projects.Description,
            StartDate = projects.StartDate,
            EndDate = projects.EndDate,
            ServiceId = projects.ServiceId,
            CustomerId = projects.CustomerId,
            EmployeeId = projects.EmployeeId,
            StatusId = projects.StatusId,
        };
    }
}
