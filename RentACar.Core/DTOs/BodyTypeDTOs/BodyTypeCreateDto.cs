using System.ComponentModel.DataAnnotations;

namespace RentACar.Core.DTOs.BodyTypeDTOs
{
    public class BodyTypeCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
