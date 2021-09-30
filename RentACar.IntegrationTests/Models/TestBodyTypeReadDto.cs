using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentACar.Core.DTOs.BodyTypeDTOs;

namespace RentACar.IntegrationTests.Models
{
    public class TestBodyTypeReadDto : BodyTypeReadDto
    {
        public TestBodyTypeReadDto CloneWith(Action<TestBodyTypeReadDto> changes)
        {
            var clone = (TestBodyTypeReadDto)MemberwiseClone();

            changes(clone);

            return clone;
        }
    }
}
