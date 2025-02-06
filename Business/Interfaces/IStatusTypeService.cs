using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IStatusTypeService
{
    Task<StatusTypes> CreateStatus(StatusTypeRegForm form);
    Task<StatusTypes> UpdateStatus(StatusTypes status);
    Task<bool> DeleteStatus(int id);
    Task<StatusTypes> GetStatusById(int id);
    Task<IEnumerable<StatusTypes>> GetStatus(string? search);
}
