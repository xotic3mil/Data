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
    public class CustomerContactPersonFactory
    {
        public static CustomerContactPersonRegForm Create() => new();

        public static CustomerContactPersonEntity Create(CustomerContactPersonRegForm form) => new()
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            Phone = form.Phone,
        };

        public static CustomerContactPerson Create(CustomerContactPersonEntity entity) => new()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Phone = entity.Phone,

        };

        public static CustomerContactPersonEntity Create(CustomerContactPerson customerContactPerson) => new()
        {
            Id = customerContactPerson.Id,
            FirstName = customerContactPerson.FirstName,
            LastName = customerContactPerson.LastName,
            Email = customerContactPerson.Email,
            Phone = customerContactPerson.Phone,
        };
    }
}
