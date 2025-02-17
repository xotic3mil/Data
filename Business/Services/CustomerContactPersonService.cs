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
            try 
            {
                await _customerContactPersonRepository.BeginTransactionAsync();
                customerContactPersonEntity = await _customerContactPersonRepository.CreateAsync(customerContactPersonEntity);
                await _customerContactPersonRepository.SaveChangesAsync();
                await _customerContactPersonRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _customerContactPersonRepository.RollbackTransactionAsync();
                return null!;
            }
            return CustomerContactPersonFactory.Create(customerContactPersonEntity);
        }

        public async Task<bool> DeleteCustomerContactPerson(int id)
        {
            try 
            {
                await _customerContactPersonRepository.BeginTransactionAsync();
                var result = await _customerContactPersonRepository.DeleteAsync(x => x.Id == id);
                await _customerContactPersonRepository.SaveChangesAsync();
                await _customerContactPersonRepository.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _customerContactPersonRepository.RollbackTransactionAsync();
                return false;
            }
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

            try 
            {
                await _customerContactPersonRepository.BeginTransactionAsync();
                customerContactPersonEntity = await _customerContactPersonRepository.UpdateAsync(x => x.Id == customerContactPerson.Id, customerContactPersonEntity);
                await _customerContactPersonRepository.SaveChangesAsync();
                await _customerContactPersonRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _customerContactPersonRepository.RollbackTransactionAsync();
                return null!;
            }
            return CustomerContactPersonFactory.Create(customerContactPersonEntity);
        }
    }

}
