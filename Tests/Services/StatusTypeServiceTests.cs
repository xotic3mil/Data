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
    public class StatusTypeServiceTests
    {

        private readonly Mock<IStatusTypeRepository> _statusTypeRepositoryMock;
        private readonly IStatusTypeService _statusTypeService;

        public StatusTypeServiceTests()
        {
            _statusTypeRepositoryMock = new Mock<IStatusTypeRepository>();
            _statusTypeService = new StatusTypeService(_statusTypeRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateStatus_ShouldReturnStatus_WhenStatusDoesNotExist()
        {
            // Arrange
            var form = new StatusTypeRegForm
            {
                Status = "In Progress"
            };

            _statusTypeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<StatusTypesEntity, bool>>>()))
                .ReturnsAsync((StatusTypesEntity?)null);
            _statusTypeRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<StatusTypesEntity>()))
                .ReturnsAsync(new StatusTypesEntity { Id = 1, Status = "In Progress" });

            // Act
            var result = await _statusTypeService.CreateStatus(form);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("In Progress", result.Status);
        }

        [Fact]
        public async Task CreateStatusType_ShouldReturnNull_WhenStatusTypeExists()
        {
            // Arrange
            var form = new StatusTypeRegForm
            {
                Status = "In Progress"
            };

            _statusTypeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<StatusTypesEntity, bool>>>()))
                 .ReturnsAsync(new StatusTypesEntity { Id = 1, Status = "In Progress" });

            // Act
            var result = await _statusTypeService.CreateStatus(form);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteStatusType_ShouldReturnTrue_WhenStatusTypeExists()
        {
            // Arrange
            var statusTypeId = 1;

            _statusTypeRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<StatusTypesEntity, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _statusTypeService.DeleteStatus(statusTypeId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUnit_ShouldReturnFalse_WhenUnitDoesNotExist()
        {
            // Arrange
            var statusId = 1;

            _statusTypeRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<StatusTypesEntity, bool>>>()))
                .ReturnsAsync(false);

            // Act
            var result = await _statusTypeService.DeleteStatus(statusId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateUnit_ShouldReturnUpdatedUnit_WhenUnitExists()
        {
            // Arrange
            var status = new StatusTypes
            {
                Id = 1,
                Status = "On Going"
            };

            _statusTypeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<StatusTypesEntity, bool>>>()))
                .ReturnsAsync(new StatusTypesEntity { Id = 1, Status = "In Progress" });
            _statusTypeRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<StatusTypesEntity, bool>>>(), It.IsAny<StatusTypesEntity>()))
                .ReturnsAsync(new StatusTypesEntity { Id = 1, Status = "On Going" });

            // Act
            var result = await _statusTypeService.UpdateStatus(status);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("On Going", result.Status);
        }

    }
}
