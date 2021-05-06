using Microsoft.EntityFrameworkCore;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(RentACarContext context) : base(context)
        {

        }

        public async Task<Country> GetByName(string name) => await entities.FirstOrDefaultAsync(c => c.Name == name);
       
    }
}
