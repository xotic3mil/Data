using Business.Dtos;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ICustomersService
    {

        public Task<Customers> CreateCustomer(CustomerRegForm form);

        public Task<Customers> GetCustomerId(int id);

        public Task<IEnumerable<Customers>> GetCustomers(string? search);

        public Task<Customers> UpdateCustomer(Customers customers);

        public Task<bool> DeleteCustomer(int id);

    }
}
