using System.ComponentModel.DataAnnotations;

namespace RentACar.Core.DTOs.BodyTypeDTOs
{
    public class BodyTypeReadDto
    {
        [Required(ErrorMessage = "Invalid Id")]
        public long Id { get; set; }
        [Required(ErrorMessage = "Invalid name")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
