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
    public class CustomerContactPersonFactoryTests
    {


        [Fact]
        public void Create_ShouldReturnCustomerContactPersonRegForm()
        {

            // Act
            var result = CustomerContactPersonFactory.Create();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CustomerContactPersonRegForm>(result);
        }


        [Fact]
        public void Create_FromCustomerContactPersonRegForm_ShouldReturnCustomerContactPersonEntity()
        {
            // Arrange
            var form = new CustomerContactPersonRegForm
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com",
                Phone = "100",

            };

            // Act
            var result = CustomerContactPersonFactory.Create(form);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CustomerContactPersonEntity>(result);
            Assert.Equal(form.FirstName, result.FirstName);
            Assert.Equal(form.LastName, result.LastName);
            Assert.Equal(form.Email, result.Email);
            Assert.Equal(form.Phone, result.Phone);

        }

        [Fact]
        public void Create_FromCustomerContactPersonEntity_ShouldReturnCustomerContactPerson()
        {
            // Arrange
            var entity = new CustomerContactPersonEntity
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com",
                Phone = "100",

            };

            // Act
            var result = CustomerContactPersonFactory.Create(entity);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CustomerContactPerson>(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.FirstName, result.FirstName);
            Assert.Equal(entity.LastName, result.LastName);
            Assert.Equal(entity.Email, result.Email);
            Assert.Equal(entity.Phone, result.Phone);


        }

        [Fact]
        public void Create_FromUnit_ShouldReturnCustomerContactPersonEntity()
        {
            // Arrange
            var contactPerson = new CustomerContactPerson
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com",
                Phone = "100",
            };


            // Act
            var result = CustomerContactPersonFactory.Create(contactPerson);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CustomerContactPersonEntity>(result);
            Assert.Equal(contactPerson.Id, contactPerson.Id);
            Assert.Equal(contactPerson.FirstName, contactPerson.FirstName);
            Assert.Equal(contactPerson.LastName, contactPerson.LastName);
            Assert.Equal(contactPerson.Email, contactPerson.Email);
            Assert.Equal(contactPerson.Phone, contactPerson.Phone);


        }
    }


}

