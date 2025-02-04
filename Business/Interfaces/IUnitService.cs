using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IUnitService
{
    public Task<Units> CreateUnit(UnitRegForm form);

    public Task<Units> GetUnitId(int id);

    public Task<IEnumerable<Units>> GetUnits(string? search);

    public Task<Units> UpdateUnit(Units units);

    public Task<bool> DeleteUnit(int id);
}
