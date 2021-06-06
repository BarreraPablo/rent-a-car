using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByUsername(UserLoginReqDto userLogin);
        Task RegisterUser(User user);
        Task SetTokenRecovery(string email, string tokenRecovery);
        Task UpdatePasswordWithRecoveryToken(string recoveryToken, string newPassword);
    }
}