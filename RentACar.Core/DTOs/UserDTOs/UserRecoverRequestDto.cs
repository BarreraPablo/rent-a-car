using System.ComponentModel.DataAnnotations;

namespace RentACar.Core.DTOs.UserDTOs
{
    public class UserRecoverRequestDto
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Email address invalid")]
        public string EmailAddress { get; set; }
    }
}
