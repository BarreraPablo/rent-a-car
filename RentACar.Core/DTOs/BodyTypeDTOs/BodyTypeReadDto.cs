using System.ComponentModel.DataAnnotations;

namespace RentACar.Core.DTOs.BodyTypeDTOs
{
    public class BodyTypeReadDto
    {
        public long id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
