using Business.Dtos;
using Business.Factories;
using Business.Models;
using Data.Entities;


namespace Business.Test.Factories;

public class RoleFactoryTests
{

    [Fact]
    public void Create_ShouldReturnRolesRegForm()
    {

        // Act
        var result = RoleFactory.Create();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<RolesRegForm>(result);
    }

    [Fact]
    public void Create_FromRolesRegForm_ShouldReturnUnitEntity()
    {
        // Arrange
        var form = new RolesRegForm
        {
            RoleName = "Project Manager"

        };

        // Act
        var result = RoleFactory.Create(form);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<RolesEntity>(result);
        Assert.Equal(form.RoleName, result.RoleName);

    }

    [Fact]
    public void Create_FromRoleEntity_ShouldReturRoles()
    {
        // Arrange
        var entity = new RolesEntity
        {
            Id = 1,
            RoleName = "Project Manager",

        };

        // Act
        var result = RoleFactory.Create(entity);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Roles>(result);
        Assert.Equal(entity.Id, result.Id);
        Assert.Equal(entity.RoleName, result.RoleName);

    }

    [Fact]
    public void Create_FromRoles_ShouldReturRolesEntity()
    {
        // Arrange
        var role = new Roles
        {
            Id = 1,
            RoleName = "Project Manager",
        };


        // Act
        var result = RoleFactory.Create(role);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<RolesEntity>(result);
        Assert.Equal(role.Id, result.Id);
        Assert.Equal(role.RoleName, result.RoleName);

    }
}
