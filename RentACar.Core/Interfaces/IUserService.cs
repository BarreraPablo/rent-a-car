using RentACar.Core.Entities;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByUsername(UserLogin userLogin);
        Task RegisterUser(User user);
    }
}