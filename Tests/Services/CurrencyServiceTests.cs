using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entities;
using Data.Interfaces;
using Moq;
using System.Linq.Expressions;


namespace Business.Test.Services
{
    public class CurrencyServiceTests
    {
        private readonly Mock<ICurrencyRepository> _currencyRepositoryMock;
        private readonly ICurrenciesService _currencyService;
        public CurrencyServiceTests()
        {
            _currencyRepositoryMock = new Mock<ICurrencyRepository>();
            _currencyService = new CurrencyService(_currencyRepositoryMock.Object);
        }


        [Fact]
        public async Task CreatCurrency_ShouldReturnUnit_WhenCurrencyDoesNotExist()
        {
            // Arrange
            var form = new CurrencyRegForm
            {
                Currency = "SEK"
            };

            _currencyRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CurrencyEntity, bool>>>()))
                .ReturnsAsync((CurrencyEntity)null);
            _currencyRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<CurrencyEntity>()))
                .ReturnsAsync(new CurrencyEntity { Id = 1, Currency = "SEK" });

            // Act
            var result = await _currencyService.CreateCurrency(form);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SEK", result.Currency);
        }

        [Fact]
        public async Task CreateCurrency_ShouldReturnNull_WhenCurrencyExists()
        {
            // Arrange
            var form = new CurrencyRegForm
            {
                Currency = "SEK"
            };

            _currencyRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CurrencyEntity, bool>>>()))
                .ReturnsAsync(new CurrencyEntity { Id = 1, Currency = "SEK" });

            // Act
            var result = await _currencyService.CreateCurrency(form);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteCurrency_ShouldReturnTrue_WhenCurrencyExists()
        {
            // Arrange
            var currencyId = 1;

            _currencyRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<CurrencyEntity, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _currencyService.DeleteCurrency(currencyId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCurrency_ShouldReturnFalse_WhenCurrencyDoesNotExist()
        {
            // Arrange
            var currencyId = 1;

            _currencyRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<CurrencyEntity, bool>>>()))
                .ReturnsAsync(false);

            // Act
            var result = await _currencyService.DeleteCurrency(currencyId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateCurrency_ShouldReturnUpdateCurrency_WhenCurrencyExists()
        {
            // Arrange
            var currency = new Currencies
            {
                Id = 1,
                Currency = "SEK"
            };
   


            _currencyRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<CurrencyEntity, bool>>>()))
                .ReturnsAsync(new CurrencyEntity { Id = 1, Currency = "Project Manager" });
            _currencyRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<CurrencyEntity, bool>>>(), It.IsAny<CurrencyEntity>()))
                .ReturnsAsync(new CurrencyEntity { Id = 1, Currency = "SEK" });

            // Act
            var result = await _currencyService.UpdateCurrency(currency);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SEK", result.Currency);
        }
    }
}
