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
    public class EmployeeServiceTests
    {

        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly IEmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var form = new EmployeeRegForm
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                RoleId = 1,
                ContractStartDate = DateOnly.FromDateTime(DateTime.Now)
            };

            _employeeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
                .ReturnsAsync((EmployeeEntity)null);
            _employeeRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<EmployeeEntity>()))
                .ReturnsAsync(new EmployeeEntity { Id = 1, FirstName = "John", LastName = "Doe" });

            // Act
            var result = await _employeeService.CreateEmployee(form);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnNull_WhenEmployeeExists()
        {
            // Arrange
            var form = new EmployeeRegForm
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                RoleId = 1,
                ContractStartDate = DateOnly.FromDateTime(DateTime.Now)
            };

            _employeeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))

                .ReturnsAsync(new EmployeeEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", });

            // Act
            var result = await _employeeService.CreateEmployee(form);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnTrue_WhenEmployeeExists()
        {
            // Arrange
            var employeeId = 1;

            _employeeRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _employeeService.DeleteEmployee(employeeId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnFalse_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = 1;

            _employeeRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
                .ReturnsAsync(false);

            // Act
            var result = await _employeeService.DeleteEmployee(employeeId);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public async Task UpdateEmployee_ShouldReturnUpdatedEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employee = new Employee
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                RoleId = 1,
                ContractStartDate = DateOnly.FromDateTime(DateTime.Now)
            };

            _employeeRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
                .ReturnsAsync(new EmployeeEntity { Id = 1, FirstName = "John", LastName = "Doe" });
            _employeeRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<EmployeeEntity, bool>>>(), It.IsAny<EmployeeEntity>()))
                .ReturnsAsync(new EmployeeEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" });

            // Act
            var result = await _employeeService.UpdateEmployee(employee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("john.doe@example.com", result.Email);
        }

    }

}

