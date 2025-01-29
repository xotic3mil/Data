using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;

    public async Task<StatusTypes> CreateStatus(StatusTypeRegForm form)
    {
            var statusEntity = StatusTypeFactory.Create(form);
            statusEntity = await _statusTypeRepository.CreateAsync(statusEntity);
            return StatusTypeFactory.Create(statusEntity);

    }

    public async Task<bool> DeleteStatus(int id)
    {
        var result = await _statusTypeRepository.DeleteAsync(x => x.Id == id);
        return result;
    }

    public async Task<IEnumerable<StatusTypes>> GetStatus()
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
        var statusEntity = StatusTypeFactory.Create(status, status.Id);
        statusEntity = await _statusTypeRepository.UpdateAsync(x => x.Id == status.Id, statusEntity);
        return StatusTypeFactory.Create(statusEntity);
    }
}
