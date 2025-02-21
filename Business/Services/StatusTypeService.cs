using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;

    public async Task<StatusTypes> CreateStatus(StatusTypeRegForm form)
    {
        var statusTypeEntity = await _statusTypeRepository.GetAsync(x => x.Status == form.Status);

        if (statusTypeEntity != null)
        {
            return null!;
        }

        statusTypeEntity = StatusTypeFactory.Create(form);
        try 
        {
            await _statusTypeRepository.BeginTransactionAsync();
            statusTypeEntity = await _statusTypeRepository.CreateAsync(statusTypeEntity);
            await _statusTypeRepository.SaveChangesAsync();
            await _statusTypeRepository.CommitTransactionAsync();

        }
        catch (Exception ex)
        {
            await _statusTypeRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return null!;
        }
        return StatusTypeFactory.Create(statusTypeEntity);
    }

    public async Task<bool> DeleteStatus(int id)
    {

        try 
        {
            await _statusTypeRepository.BeginTransactionAsync();
            var result = await _statusTypeRepository.DeleteAsync(x => x.Id == id);
            await _statusTypeRepository.SaveChangesAsync();
            await _statusTypeRepository.CommitTransactionAsync();
            return result;

        }

        catch (Exception ex)
        {
            await _statusTypeRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<StatusTypes>> GetStatus(string? search)
    {
        var entities = await _statusTypeRepository.GetAllAsync();
        return entities.Select(StatusTypeFactory.Create);
    }

    public async Task<StatusTypes> GetStatusById(int id)
    {
        var serviceEntity = await _statusTypeRepository.GetAsync(x => x.Id == id);
        return StatusTypeFactory.Create(serviceEntity) ?? null!;
    }

    public async Task<StatusTypes> UpdateStatus(StatusTypes status)
    {
        var statusEntity = StatusTypeFactory.Create(status);
        try 
        {
            await _statusTypeRepository.BeginTransactionAsync();
            statusEntity = await _statusTypeRepository.UpdateAsync(x => x.Id == status.Id, statusEntity);
            await _statusTypeRepository.SaveChangesAsync();
            await _statusTypeRepository.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _statusTypeRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return null!;
        }
        return StatusTypeFactory.Create(statusEntity);
    }
}
