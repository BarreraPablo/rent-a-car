using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using System.Threading.Tasks;
using RentACar.Core.Enumerations;

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

            if(user.EmailAddress != null)
            {
                validateUser = await unitOfWork.UserRepository.GetByEmail(user.EmailAddress);

                if(validateUser != null)
                {
                    throw new BussinessException("There is already an account with this email address");
                }
            }

            user.Role = RoleType.Consumer;

            await unitOfWork.UserRepository.Add(user);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task SetTokenRecovery(string email, string tokenRecovery)
        {
            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(tokenRecovery))
            {
                throw new ArgumentNotDefinedException();
            }

            User user = await unitOfWork.UserRepository.GetByEmail(email);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            user.TokenRecovery = tokenRecovery;

            await unitOfWork.UserRepository.Update(user);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePasswordWithRecoveryToken(string recoveryToken, string newPassword)
        {
            if(string.IsNullOrWhiteSpace(recoveryToken) || string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentNotDefinedException();
            }

            User user = await unitOfWork.UserRepository.GetByRecoveryToken(recoveryToken);

            if (user == null)
            {
                throw new ExpiredRecoveryTokenException();
            }

            user.Password = newPassword;
            user.TokenRecovery = null;

            await unitOfWork.UserRepository.Update(user);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
