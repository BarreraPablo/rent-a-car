using Microsoft.EntityFrameworkCore;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(RentACarContext context) : base(context)
        {

        }

        public async Task<RefreshToken> GetByToken(string token)
        {
            return await entities.SingleAsync(r => r.Token == token);
        }
    }
}
