using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        Task<UserLoginResDto> ProcessRefreshToken(string token, string ipAddress);
        Task<UserLoginResDto> GetAuthTokens(User user, string ipAddress);
    }
}