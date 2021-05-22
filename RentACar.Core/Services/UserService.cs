using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
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

        public async Task<User> GetByUsername(UserLoginReqDto userLogin)
        {
            if(userLogin == null)
            {
                throw new ArgumentNotDefinedException();
            }

            return await unitOfWork.UserRepository.GetByUsername(userLogin.Username);
        }

        public async Task RegisterUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNotDefinedException();
            }

            var validateUser = await unitOfWork.UserRepository.GetByUsername(user.Username);

            if(validateUser != null)
            {
                throw new BussinessException("The username is already taken");
            }

            validateUser = await unitOfWork.UserRepository.GetByEmail(user.EmailAddress);

            if(validateUser != null)
            {
                throw new BussinessException("There is already an account with this email address");
            }

            await unitOfWork.UserRepository.Add(user);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
