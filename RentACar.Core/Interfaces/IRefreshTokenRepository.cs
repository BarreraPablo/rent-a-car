using RentACar.Core.Entities;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IRefreshTokenRepository : ICreateRepository<RefreshToken>
    {
        Task<RefreshToken> GetByToken(string token);
    }
}