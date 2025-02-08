using Business.Dtos;
using Business.Factories;
using Business.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Test.Factories
{
    public class UnitFactoryTests
    {

        [Fact]
        public void Create_ShouldReturnUnitRegForm()
        {

            // Act
            var result = UnitFactory.Create();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UnitRegForm>(result);
        }


        [Fact]
        public void Create_FromUnitRegForm_ShouldReturnUnitEntity()
        {
            // Arrange
            var form = new UnitRegForm
            {
                Unit = "100"

            };

            // Act
            var result = UnitFactory.Create(form);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UnitEntity>(result);
            Assert.Equal(form.Unit, result.Unit);
    
        }

        [Fact]
        public void Create_FromUnitEntity_ShouldReturnUnits()
        {
            // Arrange
            var entity = new UnitEntity
            {
                Id = 1,
                Unit = "/Months",

            };

            // Act
            var result = UnitFactory.Create(entity);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Units>(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Unit, result.Unit);

        }

        [Fact]
        public void Create_FromUnit_ShouldReturnUnitEntity()
        {
            // Arrange
            var unit = new Units
            {
                Id = 1,
                Unit = "100",
            };


            // Act
            var result = UnitFactory.Create(unit);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UnitEntity>(result);
            Assert.Equal(unit.Id, result.Id);
            Assert.Equal(unit.Unit, result.Unit);

        }
    }
}

