using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class ServicesService(IServiceRepository serviceRepository) : IServicesService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<Service> CreateService(ServiceRegForm form)
    {
        var serviceEntity = await _serviceRepository.GetAsync(x => x.ServiceName == form.ServiceName);

        if (serviceEntity != null)
        {
            return null!;
        }

        serviceEntity = ServiceFactory.Create(form);
        serviceEntity = await _serviceRepository.CreateAsync(serviceEntity);
        return ServiceFactory.Create(serviceEntity);
    }

    public async Task<bool> DeleteService(int id)
    {
        var result = await _serviceRepository.DeleteAsync(x => x.Id == id);
        return result;
    }

    public async Task<Service> GetServiceId(int id)
    {
        var serviceEntity = await _serviceRepository.GetAsync(x => x.Id == id);
        return ServiceFactory.Create(serviceEntity) ?? null!;
    }

    public async Task<IEnumerable<Service>> GetServices(string? search)
    {
        var services = await _serviceRepository.GetAllAsync();
        return services.Select(ServiceFactory.Create);
    }

    public async Task<Service> UpdateService(Service service)
    {
        var serviceEntity = ServiceFactory.Create(service, service.Id);
        serviceEntity = await _serviceRepository.UpdateAsync(x => x.Id == service.Id, serviceEntity);
        return ServiceFactory.Create(serviceEntity);
    }
}
