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
    public class CurrencyFactoryTests
    {
        [Fact]
        public void Create_ShouldReturnCurrencyRegForm()
        {

            // Act
            var result = CurrencyFactory.Create();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CurrencyRegForm>(result);
        }


        [Fact]
        public void Create_FromCurrencyRegForm_ShouldReturnCurrencyEntity()
        {
            // Arrange
            var form = new CurrencyRegForm
            {
                Currency = "100"

            };

            // Act
            var result = CurrencyFactory.Create(form);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CurrencyEntity>(result);
            Assert.Equal(form.Currency, result.Currency);

        }

        [Fact]
        public void Create_FromCurrencyEntity_ShouldReturnCurrencies()
        {
            // Arrange
            var entity = new CurrencyEntity
            {
                Id = 1,
                Currency = "100"

            };

            // Act
            var result = CurrencyFactory.Create(entity);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Currencies>(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Currency, result.Currency);

        }

        [Fact]
        public void Create_FromUnit_ShouldReturnCurrencyEntity()
        {
            // Arrange
            var unit = new Currencies
            {
                Id = 1,
                Currency = "100",
            };


            // Act
            var result = CurrencyFactory.Create(unit);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CurrencyEntity>(result);
            Assert.Equal(unit.Id, result.Id);
            Assert.Equal(unit.Currency, result.Currency);

        }
    }
}

