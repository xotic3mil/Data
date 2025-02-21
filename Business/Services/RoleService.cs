using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<Roles> CreateRole(RolesRegForm form)
        {
            var roleEntity = await _roleRepository.GetAsync(x => x.RoleName == form.RoleName);

            if (roleEntity != null)
            {
                return null!;
            }

            roleEntity = RoleFactory.Create(form);

            try 
            {
                await _roleRepository.BeginTransactionAsync();
                roleEntity = await _roleRepository.CreateAsync(roleEntity);
                await _roleRepository.SaveChangesAsync();
                await _roleRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _roleRepository.RollbackTransactionAsync();
                Debug.WriteLine(ex.Message);
                return null!;
            }
            return RoleFactory.Create(roleEntity);
        }

        public async Task<Roles> UpdateRole(Roles role)
        {
            var roleEntity = RoleFactory.Create(role);

            try 
            {
                await _roleRepository.BeginTransactionAsync();
                roleEntity = await _roleRepository.UpdateAsync(x => x.Id == role.Id, roleEntity);
                await _roleRepository.SaveChangesAsync();
                await _roleRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _roleRepository.RollbackTransactionAsync();
                Debug.WriteLine(ex.Message);
                return null!;
            }
            return RoleFactory.Create(roleEntity);
        }

        public async Task<bool> DeleteRole(int id)
        {
            try 
            {
                await _roleRepository.BeginTransactionAsync();
                var result = await _roleRepository.DeleteAsync(x => x.Id == id);
                await _roleRepository.SaveChangesAsync();
                await _roleRepository.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _roleRepository.RollbackTransactionAsync();
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Roles> GetRoleById(int id)
        {
            var roleEntity = await _roleRepository.GetAsync(x => x.Id == id);
            return RoleFactory.Create(roleEntity) ?? null!;
        }

        public async Task<IEnumerable<Roles>> GetRoles()
        {
            var entities = await _roleRepository.GetAllAsync();
            return entities.Select(RoleFactory.Create);
        }
    }
}