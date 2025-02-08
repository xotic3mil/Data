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
    public class ProjectFactoryTests
    {
        [Fact]
        public void Create_ShouldReturnProjectRegForm()
        {
            // Act
            var result = ProjectFactory.Create();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProjectRegForm>(result);
        }

        [Fact]
        public void Create_FromProjectRegForm_ShouldReturnProjectEntity()
        {
            // Arrange
            var form = new ProjectRegForm
            {
                Name = "Project",
                Description = "Project Description",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                ServiceId = 1,
                CustomerId = 1,
                EmployeeId = 1,
                StatusId = 1
            };

            // Act
            var result = ProjectFactory.Create(form);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProjectEntity>(result);
            Assert.Equal(form.Name, result.Name);
            Assert.Equal(form.Description, result.Description);
            Assert.Equal(form.StartDate, result.StartDate);
            Assert.Equal(form.EndDate, result.EndDate);
            Assert.Equal(form.ServiceId, result.ServiceId);
            Assert.Equal(form.CustomerId, result.CustomerId);
            Assert.Equal(form.EmployeeId, result.EmployeeId);
            Assert.Equal(form.StatusId, result.StatusId);
        }

        [Fact]
        public void Create_FromProjectEntity_ShouldReturnProject()
        {
            // Arrange
            var entity = new ProjectEntity
            {
                Id = 1,
                Name = "Project",
                Description = "Project Description",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                ServiceId = 1,
                Service = new ServiceEntity { Id = 1, ServiceName = "Service" },
                CustomerId = 1,
                Customer = new CustomerEntity { Id = 1, CompanyName = "Nexon AB" },
                EmployeeId = 1,
                Employee = new EmployeeEntity { Id = 1, FirstName = "John", LastName = "Doe" },
                StatusId = 1,
                Status = new StatusTypesEntity { Id = 1, Status = "Status" }
            };


            // Act
            var result = ProjectFactory.Create(entity);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Projects>(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(entity.Description, result.Description);
            Assert.Equal(entity.ServiceId, result.ServiceId);
            Assert.NotNull(result.Service);
            Assert.Equal(entity.CustomerId, result.CustomerId);
            Assert.NotNull(result.Customers);
            Assert.Equal(entity.EmployeeId, result.EmployeeId);
            Assert.NotNull(result.Employee);
            Assert.Equal(entity.StatusId, result.StatusId);
            Assert.NotNull(result.Status);
        }

        [Fact]
        public void Create_FromProjecteAndEntityIds_ShouldReturnEmployeeEntity()
        {
            // Arrange
            var projects = new Projects
            {
                Id = 1,
                Name = "Project",
                Description = "Project Description",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                ServiceId = 1,
                CustomerId = 1,
                EmployeeId = 1,
                StatusId = 1
            };
            int ServiceId = 1;
            int CustomerId = 1;
            int EmployeeId = 1;
            int StatusId = 1;

            // Act
            var result = ProjectFactory.Create(projects);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProjectEntity>(result);
            Assert.Equal(projects.Id, result.Id);
            Assert.Equal(projects.Name, result.Name);
            Assert.Equal(projects.Description, result.Description);
            Assert.Equal(projects.StartDate, result.StartDate);
            Assert.Equal(projects.EndDate, result.EndDate);
            Assert.Equal(ServiceId, result.ServiceId);
            Assert.Equal(CustomerId, result.CustomerId);
            Assert.Equal(EmployeeId, result.EmployeeId);
            Assert.Equal(StatusId, result.StatusId);
        }

    }
}
