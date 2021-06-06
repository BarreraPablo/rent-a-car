using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.DTOs.UserDTOs
{
    public class UserPasswordRecoveryDto
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password field must be between 6 and 10 characters")]
        public string Password { get; set; }

        [Required]
        public string RecoveryToken { get; set; }
    }
}
