using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        Task<UserLoginResDto> ProcessRefreshToken(string token);
        Task<UserLoginResDto> GetAuthTokens(User user);
        public string GenerateToken(User user);
    }
}