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
    public class ProjectServiceTests
    {

        private readonly Mock<IProjectRespository> _projectRepositoryMock;
        private readonly IProjectsService _projectService;

        public ProjectServiceTests()
        {
            _projectRepositoryMock = new Mock<IProjectRespository>();
            _projectService = new ProjectService(_projectRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateProject_ShouldReturnProject_WhenProjectDoesNotExist()
        {
            // Arrange
            var form = new ProjectRegForm
            {
                Name = "John",
                Description = "Short Description of the project",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                StatusId = 1,
                CustomerId = 1,
                EmployeeId = 1,
                ServiceId = 1,
            };

            _projectRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<ProjectEntity, bool>>>()))
                .ReturnsAsync((ProjectEntity)null);
            _projectRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<ProjectEntity>()))
                .ReturnsAsync(new ProjectEntity
                {
                    Name = "Project 1",
                    Description = "Short Description of the project",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now),
                    StatusId = 1,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                });

            // Act
            var result = await _projectService.CreateProject(form);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Project 1", result.Name);
            Assert.Equal("Short Description of the project", result.Description);
            Assert.Equal(DateOnly.FromDateTime(DateTime.Now), result.StartDate);
            Assert.Equal(DateOnly.FromDateTime(DateTime.Now), result.EndDate);
            Assert.Equal(1, result.StatusId);
            Assert.Equal(1, result.CustomerId);
            Assert.Equal(1, result.EmployeeId);
            Assert.Equal(1, result.ServiceId);
        }

        [Fact]
        public async Task CreateProject_ShouldReturnNull_WhenProjectExists()
        {
            // Arrange
            var form = new ProjectRegForm
            {
                Name = "Project 1",
                Description = "Short Description of the project",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                StatusId = 1,
                CustomerId = 1,
                EmployeeId = 1,
                ServiceId = 1,
            };

            _projectRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<ProjectEntity, bool>>>()))

                .ReturnsAsync(new ProjectEntity
                {
                    Name = "Project 1",
                    Description = "Short Description of the project",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now),
                    StatusId = 1,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                });

            // Act
            var result = await _projectService.CreateProject(form);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteProject_ShouldReturnTrue_WhenProjectExists()
        {
            // Arrange
            var ProjectId = 1;

            _projectRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<ProjectEntity, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _projectService.DeleteProject(ProjectId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteProject_ShouldReturnFalse_WhenProjectDoesNotExist()
        {
            // Arrange
            var ProjectId = 1;

            _projectRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<ProjectEntity, bool>>>()))
                .ReturnsAsync(false);

            // Act
            var result = await _projectService.DeleteProject(ProjectId);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public async Task UpdateProject_ShouldReturnUpdatedProject_WhenProjectExists()
        {
            // Arrange
            var Project = new Projects
            {
                Id = 1,
                Name = "Project 1",
                Description = "Short Description of the project",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                StatusId = 1,
                CustomerId = 1,
                EmployeeId = 1,
                ServiceId = 1,
            };

            _projectRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<ProjectEntity, bool>>>()))
                .ReturnsAsync(new ProjectEntity
                {
                    Id = 1,
                    Name = "Project 1",
                    Description = "Short Description of the project",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now),
                    StatusId = 1,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                     });
            _projectRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<ProjectEntity, bool>>>(), It.IsAny<ProjectEntity>()))
                .ReturnsAsync(new ProjectEntity
                {
                    Id = 1,
                    Name = "Project 1",
                    Description = "Short Description of the project",
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    EndDate = DateOnly.FromDateTime(DateTime.Now),
                    StatusId = 1,
                    CustomerId = 1,
                    EmployeeId = 1,
                    ServiceId = 1,
                });

            // Act
            var result = await _projectService.UpdateProject(Project);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Project 1", result.Name);
            Assert.Equal("Short Description of the project", result.Description);
            Assert.Equal(DateOnly.FromDateTime(DateTime.Now), result.StartDate);
            Assert.Equal(DateOnly.FromDateTime(DateTime.Now), result.EndDate);
            Assert.Equal(1, result.StatusId);
            Assert.Equal(1, result.CustomerId);
            Assert.Equal(1, result.EmployeeId);
            Assert.Equal(1, result.ServiceId);
        }

    }
}

