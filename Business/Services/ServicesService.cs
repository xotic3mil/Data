﻿using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

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
        try 
        {
            await _serviceRepository.BeginTransactionAsync();
            serviceEntity = await _serviceRepository.CreateAsync(serviceEntity);
            await _serviceRepository.SaveChangesAsync();
            await _serviceRepository.CommitTransactionAsync();

        }
        catch (Exception ex)
        {
            await _serviceRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return null!;
        }
            return ServiceFactory.Create(serviceEntity);
    }

    public async Task<bool> DeleteService(int id)
    {

        try
        {
            await _serviceRepository.BeginTransactionAsync();
            var result = await _serviceRepository.DeleteAsync(x => x.Id == id);
            await _serviceRepository.SaveChangesAsync();
            await _serviceRepository.CommitTransactionAsync();
            return result;

        }

        catch (Exception ex) 
        {
            await _serviceRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return false;
        }
        
       
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

        try 
        {
            await _serviceRepository.BeginTransactionAsync();
            serviceEntity = await _serviceRepository.UpdateAsync(x => x.Id == service.Id, serviceEntity);
            await _serviceRepository.SaveChangesAsync();
            await _serviceRepository.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _serviceRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return null!;
        }
        return ServiceFactory.Create(serviceEntity);
    }
}
