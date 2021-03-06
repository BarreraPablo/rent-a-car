using RentACar.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface ICarService
    {
        Task Create(Car car);
        IEnumerable<Car> GetAll(bool onlyAvailable);
        Task Update(Car car);
        Task<Car> GetById(long id);
        Task<Car> GetByIdWith(long id, bool bodyType, bool brand);
    }
}