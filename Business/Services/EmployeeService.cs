using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<Employee> CreateEmployee(EmployeeRegForm form)
    {
        var employeeEntity = await _employeeRepository.GetAsync(x => x.Email == form.Email);

        if (employeeEntity != null)
        {
            return null!;
        }

        employeeEntity = EmployeeFactory.Create(form);
        employeeEntity = await _employeeRepository.CreateAsync(employeeEntity);
        return EmployeeFactory.Create(employeeEntity);
    }

    public async Task<Employee> GetEmployeeId(int id)
    {
        var employeeEntity = await _employeeRepository.GetAsync(x => x.Id == id);
        return EmployeeFactory.Create(employeeEntity) ?? null!;
    }

    public async Task<Employee> GetEmployeeEmail(string email)
    {
        var employeeEntity = await _employeeRepository.GetAsync(x => x.Email == email);
        return EmployeeFactory.Create(employeeEntity) ?? null!;
    }

    public async Task<IEnumerable<Employee>> GetEmployees(string? search)
    {
        var employees = await _employeeRepository.GetAllAsync();


        if (!string.IsNullOrEmpty(search))
        {
            var filteredEmployees = employees.Where(e =>
                e.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                e.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                e.Email.Contains(search, StringComparison.OrdinalIgnoreCase));

            return filteredEmployees.Select(EmployeeFactory.Create);
        }

        return employees.Select(EmployeeFactory.Create);

    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {

        var employeeEntity = EmployeeFactory.Create(employee, employee.Id);
        employeeEntity = await _employeeRepository.UpdateAsync(x => x.Id == employee.Id, employeeEntity);
        return EmployeeFactory.Create(employeeEntity);
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        var result = await _employeeRepository.DeleteAsync(x => x.Id == id);
        return result;
    }



}
