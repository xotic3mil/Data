using Business.Interfaces;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Business.Services;
using Business.Dtos;
using System.Linq.Expressions;
using Business.Models;
using Data.Entities;

namespace Business.Test.Services
{
    public class CustomerContactPersonServiceTests
    {
        private readonly Mock<ICustomerContactPersonRepository>  _customerContactPersonRepositoryMock;
        private readonly ICustomerContactPersonService _customerContactPersonService;

        public CustomerContactPersonServiceTests()
        {
            _customerContactPersonRepositoryMock = new Mock<ICustomerContactPersonRepository>();
            _customerContactPersonService = new CustomerContactPersonService( _customerContactPersonRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateCustomerContact_ShouldReturnCustomerContact_WhenCustomerContactDoesNotExist()
        {
            // Arrange
            var form = new CustomerContactPersonRegForm
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@fakemail.com",
                Phone = "1234567890",

            };

             _customerContactPersonRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CustomerContactPersonEntity, bool>>>()))
                .ReturnsAsync((CustomerContactPersonEntity)null);
             _customerContactPersonRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<CustomerContactPersonEntity>()))
                .ReturnsAsync(new CustomerContactPersonEntity { Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@fakemail.com",
                    Phone = "1234567890",
                });

            // Act
            var result = await _customerContactPersonService.CreateCustomerContactPerson(form);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("john.doe@fakemail.com", result.Email);
            Assert.Equal("1234567890", result.Phone);

        }

        [Fact]
        public async Task CreateCustomerContact_ShouldReturnNull_WhenCustomerContactExists()
        {
            // Arrange
            var form = new CustomerContactPersonRegForm
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@fakemail.com",
                Phone = "1234567890",
            };

             _customerContactPersonRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CustomerContactPersonEntity, bool>>>()))

                             .ReturnsAsync(new CustomerContactPersonEntity
                             {
                                 Id = 1,
                                 FirstName = "John",
                                 LastName = "Doe",
                                 Email = "john.doe@fakemail.com",
                                 Phone = "1234567890",
                             });

            // Act
            var result = await _customerContactPersonService.CreateCustomerContactPerson(form);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteCustomerContact_ShouldReturnTrue_WhenCustomerContactExists()
        {
            // Arrange
            var customerContactId = 1;

            _customerContactPersonRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<CustomerContactPersonEntity, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _customerContactPersonService.DeleteCustomerContactPerson(customerContactId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCustomerContact_ShouldReturnFalse_WhenCustomerContactDoesNotExist()
        {
            // Arrange
            var customerContactId = 1;

             _customerContactPersonRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<CustomerContactPersonEntity, bool>>>()))
                .ReturnsAsync(false);

            // Act
            var result = await _customerContactPersonService.DeleteCustomerContactPerson(customerContactId);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public async Task UpdateCustomerContact_ShouldReturnUpdatedCustomerContact_WhenCustomerContactExists()
        {
            // Arrange
            var customerContact = new CustomerContactPerson
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@fakemail.com",
                Phone = "1234567890",
            };

             _customerContactPersonRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CustomerContactPersonEntity, bool>>>()))
                           .ReturnsAsync(new CustomerContactPersonEntity
                           {
                               Id = 1,
                               FirstName = "John",
                               LastName = "Doe",
                               Email = "john.doe@fakemail.com",
                               Phone = "1234567890",
                           });
            _customerContactPersonRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<CustomerContactPersonEntity, bool>>>(), It.IsAny<CustomerContactPersonEntity>()))
                      .ReturnsAsync(new CustomerContactPersonEntity
                      {
                          Id = 1,
                          FirstName = "John",
                          LastName = "Doe",
                          Email = "john.doe@fakemail.com",
                          Phone = "1234567890",
                      });

            // Act
            var result = await _customerContactPersonService.UpdateCustomerContactPerson(customerContact);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("john.doe@fakemail.com", result.Email);
            Assert.Equal("1234567890", result.Phone);

        }

    }

}
