using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IEmployeeRepository : IBaseRepository<EmployeeEntity>
{

    Task <IEnumerable<EmployeeEntity>> GetEmployeesByRole(int roleId);

    Task<IEnumerable<EmployeeEntity>> GetAllEmployees();

}
