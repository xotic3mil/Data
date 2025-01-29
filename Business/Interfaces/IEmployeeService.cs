using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IEmployeeService 
{
    public Task<Employee> CreateEmployee(EmployeeRegForm form);

    public Task<Employee> GetEmployeeId(int id);

    public Task<Employee> GetEmployeeEmail(string email);

    public Task<IEnumerable<Employee>> GetEmployees(string? search);

    public Task<Employee> UpdateEmployee(Employee employee);

    public Task<bool> DeleteEmployee(int id);


}