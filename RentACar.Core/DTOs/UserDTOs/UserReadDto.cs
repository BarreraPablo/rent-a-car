using RentACar.Core.Enumerations;

namespace RentACar.Core.DTOs.UserDTOs
{
    public class UserReadDto
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
    }
}
