using System.ComponentModel.DataAnnotations;

namespace RentACar.Core.DTOs.BodyTypeDTOs
{
    public class BodyTypeReadDto
    {
        [Required(ErrorMessage = "Invalid Id")]
        public long? Id { get; set; }
        [Required(ErrorMessage = "Invalid name")]
        [MaxLength(25, ErrorMessage = "The name must be shorter than 26 characters")]
        [MinLength(2, ErrorMessage = "The name must be longer than 2 characters")]
        public string Name { get; set; }

        [MaxLength(25, ErrorMessage = "The description must be shorter than 200 characters")]
        [MinLength(2, ErrorMessage = "The description must be longer than 2 characters")]
        public string Description { get; set; }
    }
}
