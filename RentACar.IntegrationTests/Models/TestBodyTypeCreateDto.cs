using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentACar.Core.DTOs.BodyTypeDTOs;

namespace RentACar.IntegrationTests.Models
{
    public class TestBodyTypeCreateDto : BodyTypeCreateDto
    {
        public TestBodyTypeCreateDto CloneWith(Action<TestBodyTypeCreateDto> changes)
        {
            var clone = (TestBodyTypeCreateDto)MemberwiseClone();

            changes(clone);

            return clone;
        }
    }
}
