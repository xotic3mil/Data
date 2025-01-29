using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IRoleService
    {
        Task<Roles> CreateRole(RolesRegForm form);
        Task<Roles> UpdateRole(Roles role);
        Task<bool> DeleteRole(int id);
        Task<Roles> GetRoleById(int id);
        Task<IEnumerable<Roles>> GetRoles();
    }
}