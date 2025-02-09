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
    public  class UnitServiceTests
    {
        private readonly Mock<IUnitRepository> _unitRepositoryMock;
        private readonly IUnitService _unitService;

        public UnitServiceTests()
        {
            _unitRepositoryMock = new Mock<IUnitRepository>();
            _unitService = new UnitService(_unitRepositoryMock.Object);
        }


        [Fact]
        public async Task CreateUnit_ShouldReturnUnit_WhenUnitDoesNotExist()
        {
            // Arrange
            var form = new UnitRegForm
            {
                Unit = "/Months"
            };

            _unitRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<UnitEntity, bool>>>()))
                .ReturnsAsync((UnitEntity)null);
            _unitRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<UnitEntity>()))
                .ReturnsAsync(new UnitEntity { Id = 1, Unit = "/Months" });

            // Act
            var result = await _unitService.CreateUnit(form);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("/Months", result.Unit);
        }

        [Fact]
        public async Task CreateUnit_ShouldReturnNull_WhenUnitExists()
        {
            // Arrange
            var form = new UnitRegForm
            {
                Unit = "/Months"
            };

            _unitRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<UnitEntity, bool>>>()))
                .ReturnsAsync(new UnitEntity { Id = 1, Unit = "/Months" });

            // Act
            var result = await _unitService.CreateUnit(form);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteUnit_ShouldReturnTrue_WhenUnitExists()
        {
            // Arrange
            var unitId = 1;

            _unitRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<UnitEntity, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _unitService.DeleteUnit(unitId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUnit_ShouldReturnFalse_WhenUnitDoesNotExist()
        {
            // Arrange
            var unitId = 1;

            _unitRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<UnitEntity, bool>>>()))
                .ReturnsAsync(false);

            // Act
            var result = await _unitService.DeleteUnit(unitId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateUnit_ShouldReturnUpdatedUnit_WhenUnitExists()
        {
            // Arrange
            var unit = new Units
            {
                Id = 1,
                Unit = "/Years"
            };

            _unitRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<UnitEntity, bool>>>()))
                .ReturnsAsync(new UnitEntity { Id = 1, Unit = "/Months" });
            _unitRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<UnitEntity, bool>>>(), It.IsAny<UnitEntity>()))
                .ReturnsAsync(new UnitEntity { Id = 1, Unit = "/Years" });

            // Act
            var result = await _unitService.UpdateUnit(unit);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("/Years", result.Unit);
        }
    }
}
