using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public class UnitFactory
    {
        public static UnitRegForm Create() => new();

        public static UnitEntity Create(UnitRegForm form) => new()
        {
            Unit = form.Unit
        };

        public static Units Create(UnitEntity entity) => new()
        {
            Id = entity.Id,
            Unit = entity.Unit
        };

        public static UnitEntity Create(Units unit) => new()
        {
            Id = unit.Id,
            Unit = unit.Unit
        };


    }
}
