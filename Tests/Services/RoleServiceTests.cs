using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entities;
using Data.Interfaces;
using Moq;
using System.Linq.Expressions;


namespace Business.Test.Services;

public class RoleServiceTests
{

    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly IRoleService _roleService;

    public RoleServiceTests()
    {
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _roleService = new RoleService(_roleRepositoryMock.Object);
    }


    [Fact]
    public async Task CreateRole_ShouldReturnUnit_WhenRoleDoesNotExist()
    {
        // Arrange
        var form = new RolesRegForm
        {
            RoleName = "Project Manager"
        };

        _roleRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<RolesEntity, bool>>>()))
            .ReturnsAsync((RolesEntity)null);
        _roleRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<RolesEntity>()))
            .ReturnsAsync(new RolesEntity { Id = 1, RoleName = "Project Manager" });

        // Act
        var result = await _roleService.CreateRole(form);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Project Manager", result.RoleName);
    }

    [Fact]
    public async Task CreateRole_ShouldReturnNull_WhenRoleExists()
    {
        // Arrange
        var form = new RolesRegForm
        {
            RoleName = "Project Manager"
        };

        _roleRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<RolesEntity, bool>>>()))
            .ReturnsAsync(new RolesEntity { Id = 1, RoleName = "Project Manager" });

        // Act
        var result = await _roleService.CreateRole(form);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteRole_ShouldReturnTrue_WhenRoleExists()
    {
        // Arrange
        var roleId = 1;

        _roleRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<RolesEntity, bool>>>()))
            .ReturnsAsync(true);

        // Act
        var result = await _roleService.DeleteRole(roleId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteRole_ShouldReturnFalse_WhenRoleDoesNotExist()
    {
        // Arrange
        var roleId = 1;

        _roleRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<RolesEntity, bool>>>()))
            .ReturnsAsync(false);

        // Act
        var result = await _roleService.DeleteRole(roleId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateRole_ShouldReturnUpdateRole_WhenUnitExists()
    {
        // Arrange
        var role = new Roles
        {
            Id = 1,
            RoleName = "Unassigned"
        };


        _roleRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<RolesEntity, bool>>>()))
            .ReturnsAsync(new RolesEntity { Id = 1, RoleName = "Project Manager" });
        _roleRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<RolesEntity, bool>>>(), It.IsAny<RolesEntity>()))
            .ReturnsAsync(new RolesEntity { Id = 1, RoleName = "Unassigned" });

        // Act
        var result = await _roleService.UpdateRole(role);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Unassigned", result.RoleName);
    }

}

