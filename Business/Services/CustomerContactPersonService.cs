using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CustomerContactPersonService(ICustomerContactPersonRepository customerContactPersonRepository) : ICustomerContactPersonService
    {
        private readonly ICustomerContactPersonRepository _customerContactPersonRepository = customerContactPersonRepository;

        public async Task<CustomerContactPerson> CreateCustomerContactPerson(CustomerContactPersonRegForm form)
        {
            var customerContactPersonEntity = await _customerContactPersonRepository.GetAsync(x => x.Email == form.Email);

            if (customerContactPersonEntity != null)
            {
                return null!;
            }

            customerContactPersonEntity = CustomerContactPersonFactory.Create(form);
            customerContactPersonEntity = await _customerContactPersonRepository.CreateAsync(customerContactPersonEntity);
            return CustomerContactPersonFactory.Create(customerContactPersonEntity);
        }

        public async Task<bool> DeleteCustomerContactPerson(int id)
        {
            var result = await _customerContactPersonRepository.DeleteAsync(x => x.Id == id);
            return result;
        }

        public async Task<IEnumerable<CustomerContactPerson>> GetCustomerContactPerson(string? search)
        {
            var entities = await _customerContactPersonRepository.GetAllAsync();
            return entities.Select(CustomerContactPersonFactory.Create);
        }

        public async Task<CustomerContactPerson> GetCustomerContactPersonEmail(string email)
        {
            var customerContactPersonEntity = await _customerContactPersonRepository.GetAsync(x => x.Email == email);
            return CustomerContactPersonFactory.Create(customerContactPersonEntity) ?? null!;
        }

        public async Task<CustomerContactPerson> GetCustomerContactPersonId(int id)
        {
            var customerContactPersonEntity = await _customerContactPersonRepository.GetAsync(x => x.Id == id);
            return CustomerContactPersonFactory.Create(customerContactPersonEntity) ?? null!;
        }

        public async Task<CustomerContactPerson> UpdateCustomerContactPerson(CustomerContactPerson customerContactPerson)
        {
            var customerContactPersonEntity = CustomerContactPersonFactory.Create(customerContactPerson);
            customerContactPersonEntity = await _customerContactPersonRepository.UpdateAsync(x => x.Id == customerContactPerson.Id, customerContactPersonEntity);
            return CustomerContactPersonFactory.Create(customerContactPersonEntity);
        }
    }

}
