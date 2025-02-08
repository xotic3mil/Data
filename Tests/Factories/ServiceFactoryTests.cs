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
    public  class ServiceFactoryTests
    {
        [Fact]
        public void CreateService_ShouldReturnServiceRegForm() 
        {
            // Act
            var result = ServiceFactory.Create();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ServiceRegForm>(result);

        }

        [Fact]
        public void CreateService_ShouldReturnServiceEntity()
        {
            // Arrange
            var form = new ServiceRegForm
            {
                ServiceName = "Service Name",
                Price = 100.00m,
                UnitId = 1,
                CurrencyId = 1,
            };
            // Act
            var result = ServiceFactory.Create(form);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<ServiceEntity>(result);
            Assert.Equal(form.ServiceName, result.ServiceName);
            Assert.Equal(form.Price, result.Price);
            Assert.Equal(form.UnitId, result.UnitId);
            Assert.Equal(form.CurrencyId, result.CurrencyId);
        }

        [Fact]
        public void Create_FromServiceEntity_ShouldReturnService()
        {
            // Arrange
            var entity = new ServiceEntity
            {
                Id = 1,
                ServiceName = "Service Name",
                Price = 100.00m,
                UnitId = 1,
                CurrencyId = 1,
                Units = new UnitEntity { Id = 1, Unit = "1000" },
                Currencies = new CurrencyEntity { Id = 1, Currency = "USD" }
            };


            // Act
            var result = ServiceFactory.Create(entity);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Service>(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.ServiceName, result.ServiceName);
            Assert.Equal(entity.Price, result.Price);
            Assert.Equal(entity.UnitId, result.UnitId);
            Assert.Equal(entity.CurrencyId, result.CurrencyId);
            Assert.Equal(entity.Units.Unit, result.Units.Unit);
            Assert.NotNull(result.Units);
            Assert.NotNull(result.Currencies);
        }

        [Fact]
        public void Create_FromServiceAndRoleId_ShouldReturnEmployeeEntity()
        {
            // Arrange
            var service = new Service
            {
                Id = 1,
                ServiceName = "Hybrid Azure AD Join",
                Price = 100.00m,
                UnitId = 1,
                CurrencyId = 1,

            };
            int id = 1;

            // Act
            var result = ServiceFactory.Create(service, id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ServiceEntity>(result);
            Assert.Equal(service.Id, result.Id);
            Assert.Equal(service.ServiceName, result.ServiceName);
            Assert.Equal(service.Price, result.Price);

        }
    }


}

