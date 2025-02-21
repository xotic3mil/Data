using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomersService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<Customers> CreateCustomer(CustomerRegForm form)
        {
            var customerEntity = await _customerRepository.GetAsync(x => x.CompanyName == form.CompanyName);

            if (customerEntity != null)
            {
                return null!;
            }

            customerEntity = CustomerFactory.Create(form);
            try
            {
                await _customerRepository.BeginTransactionAsync();
                customerEntity = await _customerRepository.CreateAsync(customerEntity);
                await _customerRepository.SaveChangesAsync();
                await _customerRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _customerRepository.RollbackTransactionAsync();
                Debug.WriteLine(ex.Message);
                return null!;
            }

            return CustomerFactory.Create(customerEntity);
        }

        public async Task<Customers> GetCustomerId(int id)
        {
            var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
            return CustomerFactory.Create(customerEntity) ?? null!;
        }

        public async Task<IEnumerable<Customers>> GetCustomers(string? search)
        {
            var customers = await _customerRepository.GetAllAsync();



            return customers.Select(CustomerFactory.Create);

        }

        public async Task<Customers> UpdateCustomer(Customers customers)
        {

            var customerEntity = CustomerFactory.Create(customers);

            try
            {
                await _customerRepository.BeginTransactionAsync();
                customerEntity = await _customerRepository.UpdateAsync(x => x.Id == customers.Id, customerEntity);
                await _customerRepository.SaveChangesAsync();
                await _customerRepository.CommitTransactionAsync();

            }
            catch (Exception ex)
            {
                await _customerRepository.RollbackTransactionAsync();
                Debug.WriteLine(ex.Message);
                return null!;
            }

            return CustomerFactory.Create(customerEntity);
        }

        public async Task<bool> DeleteCustomer(int id)
        {

            try 
            {
                await _customerRepository.BeginTransactionAsync();
                var result = await _customerRepository.DeleteAsync(x => x.Id == id);
                await _customerRepository.SaveChangesAsync();
                await _customerRepository.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _customerRepository.RollbackTransactionAsync();
                Debug.WriteLine(ex.Message);
                return false;

            }
           
        }

    }
}
