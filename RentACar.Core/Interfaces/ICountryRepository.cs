using RentACar.Core.Entities;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface ICountryRepository : ICreateRepository<Country>, IUpdateRepository<Country>, IReadRepository<Country>
    {
        Task<Country> GetByName(string name);
    }
}
