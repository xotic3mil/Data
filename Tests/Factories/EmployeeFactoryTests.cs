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
    public class EmployeeFactoryTests
    {
        [Fact]
        public void Create_ShouldReturnEmployeeRegForm()
        {
            // Act
            var result = EmployeeFactory.Create();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EmployeeRegForm>(result);
        }

        [Fact]
        public void Create_FromEmployeeRegForm_ShouldReturnEmployeeEntity()
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

            // Act
            var result = EmployeeFactory.Create(form);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EmployeeEntity>(result);
            Assert.Equal(form.FirstName, result.FirstName);
            Assert.Equal(form.LastName, result.LastName);
            Assert.Equal(form.Email, result.Email);
            Assert.Equal(form.Phone, result.Phone);
            Assert.Equal(form.RoleId, result.RoleId);
            Assert.Equal(form.ContractStartDate, result.ContractStartDate);
        }

        [Fact]
        public void Create_FromEmployeeEntity_ShouldReturnEmployee()
        {
            // Arrange
            var entity = new EmployeeEntity
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                RoleId = 1,
                ContractStartDate = DateOnly.FromDateTime(DateTime.Now),
                Role = new RolesEntity { Id = 1, RoleName = "Admin" }
            };

            // Act
            var result = EmployeeFactory.Create(entity);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Employee>(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.FirstName, result.FirstName);
            Assert.Equal(entity.LastName, result.LastName);
            Assert.Equal(entity.Email, result.Email);
            Assert.Equal(entity.Phone, result.Phone);
            Assert.Equal(entity.RoleId, result.RoleId);
            Assert.NotNull(result.Roles);
            Assert.Equal(entity.Role.Id, result.Roles.Id);
            Assert.Equal(entity.Role.RoleName, result.Roles.RoleName);
            Assert.Equal(entity.ContractStartDate, result.ContractStartDate);
        }

        [Fact]
        public void Create_FromEmployeeAndRoleId_ShouldReturnEmployeeEntity()
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
            int roleId = 1;

            // Act
            var result = EmployeeFactory.Create(employee, roleId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EmployeeEntity>(result);
            Assert.Equal(employee.Id, result.Id);
            Assert.Equal(employee.FirstName, result.FirstName);
            Assert.Equal(employee.LastName, result.LastName);
            Assert.Equal(employee.Email, result.Email);
            Assert.Equal(employee.Phone, result.Phone);
            Assert.Equal(roleId, result.RoleId);
            Assert.Equal(employee.ContractStartDate, result.ContractStartDate);
        }
    }

}
