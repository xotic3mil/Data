using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entities;
using Data.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Test.Services
{
    public class CustomerServiceTests
    {

        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly ICustomersService _customerService;

        public CustomerServiceTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateCustomer_ShouldReturnCustomer_WhenCustomerDoesNotExist()
        {
            // Arrange
            var form = new CustomerRegForm
            {
                CompanyName = "Nexon AB",
                CustomerContactPersonId = 1,

            };

            _customerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
                .ReturnsAsync((CustomerEntity)null);
            _customerRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<CustomerEntity>()))
                .ReturnsAsync(new CustomerEntity { Id = 1, CompanyName = "Nexon AB", CustomerContactPersonId = 1 });

            // Act
            var result = await _customerService.CreateCustomer(form);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Nexon AB", result.CompanyName);
            Assert.Equal(1, result.CustomerContactPersonId);

        }

        [Fact]
        public async Task CreateCustomer_ShouldReturnNull_WhenCustomerExists()
        {
            // Arrange
            var form = new CustomerRegForm
            {
                CompanyName = "Nexon AB",
                CustomerContactPersonId = 1,
            };

            _customerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))

                .ReturnsAsync(new CustomerEntity { Id = 1, CompanyName = "John", CustomerContactPersonId = 1 });

            // Act
            var result = await _customerService.CreateCustomer(form);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturnTrue_WhenCustomerExists()
        {
            // Arrange
            var customerId = 1;

            _customerRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _customerService.DeleteCustomer(customerId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturnFalse_WhencustomerDoesNotExist()
        {
            // Arrange
            var customerId = 1;

            _customerRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
                .ReturnsAsync(false);

            // Act
            var result = await _customerService.DeleteCustomer(customerId);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public async Task UpdateCustomer_ShouldReturnUpdatedCustomer_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customers
            {
                Id = 1,
                CompanyName = "Nexon AB",
                CustomerContactPersonId = 1,
            };

            _customerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
                .ReturnsAsync(new CustomerEntity { Id = 1, CompanyName = "Nexon AB", CustomerContactPersonId = 1 });
            _customerRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<CustomerEntity, bool>>>(), It.IsAny<CustomerEntity>()))
                .ReturnsAsync(new CustomerEntity { Id = 1, CompanyName = "Nexon AB", CustomerContactPersonId = 1 });

            // Act
            var result = await _customerService.UpdateCustomer(customer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Nexon AB", result.CompanyName);
            Assert.Equal(1, result.CustomerContactPersonId);
  
        }

    }




}

