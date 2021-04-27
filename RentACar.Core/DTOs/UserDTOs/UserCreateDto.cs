using RentACar.Core.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Core.DTOs.UserDTOs
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Username field is required")]
        [StringLength(20, ErrorMessage = "Username is too long (maximium is 20 characters)")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email Address field is required")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password field must be between 6 and 10 characters")]
        public string Password { get; set; }
        public RoleType? Role { get; set; }
    }
}
