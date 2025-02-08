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
    public class ServicesServiceTests
    {

        private readonly Mock<IServiceRepository> _serviceRepositoryMock;
        private readonly IServicesService _servicesService;

        public ServicesServiceTests()
        {
            _serviceRepositoryMock = new Mock<IServiceRepository>();
            _servicesService = new ServicesService(_serviceRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var form = new ServiceRegForm
            {
                ServiceName = "M365",
                Price = 100.00m,
                CurrencyId = 1,
                UnitId = 1,

            };

            _serviceRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<ServiceEntity, bool>>>()))
                .ReturnsAsync((ServiceEntity)null);
            _serviceRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<ServiceEntity>()))
                .ReturnsAsync(new ServiceEntity { Id = 1, ServiceName = "M365", Price = 100.00m, CurrencyId = 1, UnitId = 1 });

            // Act
            var result = await _servicesService.CreateService(form);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("M365", result.ServiceName);
            Assert.Equal(100.00m, result.Price);
            Assert.Equal(1, result.CurrencyId);
            Assert.Equal(1, result.UnitId);
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnNull_WhenEmployeeExists()
        {
            // Arrange
            var form = new ServiceRegForm
            {
                ServiceName = "M365",
                Price = 100.00m,
                CurrencyId = 1,
                UnitId = 1,
            };

            _serviceRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<ServiceEntity, bool>>>()))

                .ReturnsAsync(new ServiceEntity { Id = 1, ServiceName = "M365", Price = 100.00m, CurrencyId = 1, UnitId = 1 });

            // Act
            var result = await _servicesService.CreateService(form);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnTrue_WhenEmployeeExists()
        {
            // Arrange
            var serviceId = 1;

            _serviceRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<ServiceEntity, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _servicesService.DeleteService(serviceId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnFalse_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var serviceId = 1;

            _serviceRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<ServiceEntity, bool>>>()))
                .ReturnsAsync(false);

            // Act
            var result = await _servicesService.DeleteService(serviceId);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public async Task UpdateEmployee_ShouldReturnUpdatedEmployee_WhenEmployeeExists()
        {
            // Arrange
            var service = new Service
            {
           
                ServiceName = "M365",
                Price = 100.00m,
                CurrencyId = 1,
                UnitId = 1,
            };

            _serviceRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<ServiceEntity, bool>>>()))
               .ReturnsAsync(new ServiceEntity { Id = 1, ServiceName = "M365", Price = 100.00m, CurrencyId = 1, UnitId = 1 });
            _serviceRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<ServiceEntity, bool>>>(), It.IsAny<ServiceEntity>()))
               .ReturnsAsync(new ServiceEntity { Id = 1, ServiceName = "M365", Price = 100.00m, CurrencyId = 1, UnitId = 1 });

            // Act
            var result = await _servicesService.UpdateService(service);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("M365", result.ServiceName);
            Assert.Equal(100.00m, result.Price);
            Assert.Equal(1, result.CurrencyId);
            Assert.Equal(1, result.UnitId);
        }


    }
}

