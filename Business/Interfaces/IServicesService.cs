using Business.Dtos;
using Business.Models;
using Business.Services;

namespace Business.Interfaces;

public interface IServicesService
{
    public Task<Service> CreateService(ServiceRegForm form);

    public Task<Service> GetServiceId(int id);

    public Task<IEnumerable<Service>> GetServices(string? search);

    public Task<Service> UpdateService(Service service);

    public Task<bool> DeleteService(int id);
}
