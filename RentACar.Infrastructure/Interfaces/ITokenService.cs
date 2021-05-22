using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        Task<UserLoginResDto> GenerateTokenAndRefreshToken(string token, string ipAddress);
        public string GenerateToken(User user);
        public RefreshToken GenerateRefreshToken(string ipAddress);
        Task SaveRefreshToken(RefreshToken refreshToken);
    }
}