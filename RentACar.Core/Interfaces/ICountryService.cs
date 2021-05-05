using RentACar.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface ICountryService
    {
        Task Create(Country country);
        IEnumerable<Country> GetAll();
        Task Update(Country country);
    }
}