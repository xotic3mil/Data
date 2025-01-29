using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class RoleFactory
    {
        public static RolesEntity Create(RolesRegForm form) => new()
        {
            RoleName = form.RoleName
        };

        public static RolesEntity Create(Roles role) => new()
        {
            Id = role.Id,
            RoleName = role.RoleName
        };

        public static Roles Create(RolesEntity entity) => new()
        {
            Id = entity.Id,
            RoleName = entity.RoleName ?? string.Empty
        };
    }
}