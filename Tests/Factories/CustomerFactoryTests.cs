using Business.Dtos;
using Business.Factories;
using Business.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Test.Factories
{
    public class CustomerFactoryTests
    {
        [Fact]
        public void Create_ShouldReturnCustomerRegForm() 
        {

            // Act
            var result = CustomerFactory.Create();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CustomerRegForm>(result);
        }

        [Fact]
        public void Create_FromCustomerRegForm_ShouldReturnCustomerEntity()
        {
            // Arrange
            var form = new CustomerRegForm
            {
                CompanyName = "John",
                CustomerContactPersonId = 1,
            };

            // Act
            var result = CustomerFactory.Create(form);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CustomerEntity>(result);
            Assert.Equal(form.CompanyName, result.CompanyName);
            Assert.Equal(form.CustomerContactPersonId, result.CustomerContactPersonId);
        }

        [Fact]
        public void Create_FromCustomerEntity_ShouldReturnCustomer()
        {
            // Arrange
            var entity = new CustomerEntity
            {
                Id = 1,
                CompanyName = "John",
                CustomerContactPersonId = 1,
                ContactPerson = new CustomerContactPersonEntity { Id = 1, FirstName = "John", LastName = "Doe"},
            };

            // Act
            var result = CustomerFactory.Create(entity);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Customers>(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.CompanyName, result.CompanyName);
            Assert.Equal(entity.CustomerContactPersonId, result.CustomerContactPersonId);
  
        }

        [Fact]
        public void Create_FromCustomerAndContactPerson_ShouldReturnCustomerEntity()
        {
            // Arrange
            var customer = new Customers
            {
                Id = 1,
                CompanyName = "John",
                CustomerContactPersonId = 1,
            };


            // Act
            var result = CustomerFactory.Create(customer);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CustomerEntity>(result);
            Assert.Equal(customer.Id, result.Id);
            Assert.Equal(customer.CompanyName, result.CompanyName);
            Assert.Equal(customer.CustomerContactPersonId, result.CustomerContactPersonId);

        }
    }
}

