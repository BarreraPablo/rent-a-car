using RentACar.Core.Enumerations;

namespace RentACar.Core.DTOs.UserDTOs
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public RoleType? Role { get; set; }
    }
}
