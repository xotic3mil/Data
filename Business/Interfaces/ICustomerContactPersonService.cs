using Business.Dtos;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ICustomerContactPersonService
    {
        public Task<CustomerContactPerson> CreateCustomerContactPerson(CustomerContactPersonRegForm form);

        public Task<CustomerContactPerson> GetCustomerContactPersonId(int id);

        public Task<CustomerContactPerson> GetCustomerContactPersonEmail(string email);

        public Task<IEnumerable<CustomerContactPerson>> GetCustomerContactPerson(string? search);

        public Task<CustomerContactPerson> UpdateCustomerContactPerson(CustomerContactPerson customerContactPerson);

        public Task<bool> DeleteCustomerContactPerson(int id);


    }
}
