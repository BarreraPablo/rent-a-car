using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByUsername(UserLoginDto userLogin);
        Task RegisterUser(User user);
    }
}