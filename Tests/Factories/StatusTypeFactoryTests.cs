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
    public class StatusTypeFactoryTests
    {


        [Fact]
        public void Create_ShouldReturnStatusTypeRegForm()
        {

            // Act
            var result = StatusTypeFactory.Create();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<StatusTypeRegForm>(result);
        }


        [Fact]
        public void Create_FromStatusTypeRegForm_ShouldReturnStatusTypeEntity()
        {
            // Arrange
            var form = new StatusTypeRegForm
            {
                Status = "100"

            };

            // Act
            var result = StatusTypeFactory.Create(form);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<StatusTypesEntity>(result);
            Assert.Equal(form.Status, result.Status);

        }

        [Fact]
        public void Create_FromStatusTypeEntity_ShouldReturnStatusType()
        {
            // Arrange
            var entity = new StatusTypesEntity
            {
                Id = 1,
                Status = "/Months",

            };

            // Act
            var result = StatusTypeFactory.Create(entity);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<StatusTypes>(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Status, result.Status);

        }

        [Fact]
        public void Create_Fromstatustype_ShouldReturnStatusTypeEntity()
        {
            // Arrange
            var statusType = new StatusTypes
            {
                Id = 1,
                Status = "100",
            };

            // Act
            var result = StatusTypeFactory.Create(statusType);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<StatusTypesEntity>(result);
            Assert.Equal(statusType.Id, result.Id);
            Assert.Equal(statusType.Status, result.Status);

        }
    }
}

