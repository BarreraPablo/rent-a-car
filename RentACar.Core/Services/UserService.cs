using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<User> GetLoginByCredentials(UserLogin userLogin)
        {
            return await unitOfWork.UserRepository.GetByUsername(userLogin.Username);
        }

        public async Task RegisterUser(User user)
        {
            await unitOfWork.UserRepository.Add(user);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
