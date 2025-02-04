using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UnitService(IUnitRepository unitRepository) : IUnitService
    {
        private readonly IUnitRepository _unitRepository = unitRepository;

        public async Task<Units> CreateUnit(UnitRegForm form)
        {
            var UnitEntity = await _unitRepository.GetAsync(x => x.Unit == form.Unit);

            if (UnitEntity != null)
            {
                return null!;
            }

            UnitEntity = UnitFactory.Create(form);
            UnitEntity = await _unitRepository.CreateAsync(UnitEntity);
            return UnitFactory.Create(UnitEntity);
        }

        public async Task<bool> DeleteUnit(int id)
        {
            var result = await _unitRepository.DeleteAsync(x => x.Id == id);
            return result;
        }

        public async Task<Units> GetUnitId(int id)
        {
            var UnitEntity = await _unitRepository.GetAsync(x => x.Id == id);
            return UnitFactory.Create(UnitEntity) ?? null!;
        }

        public async Task<IEnumerable<Units>> GetUnits(string? search)
        {
            var units = await _unitRepository.GetAllAsync();
            return units.Select(UnitFactory.Create);
        }

        public async Task<Units> UpdateUnit(Units units)
        {
            var UnitEntity = UnitFactory.Create(units);
            UnitEntity = await _unitRepository.UpdateAsync(x => x.Id == units.Id, UnitEntity);
            return UnitFactory.Create(UnitEntity);
        }
    }
}
