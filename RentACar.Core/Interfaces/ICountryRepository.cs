using RentACar.Core.Entities;

namespace RentACar.Core.Interfaces
{
    public interface ICountryRepository : ICreateRepository<Country>, IUpdateRepository<Country>, IReadRepository<Country>
    {
    }
}
