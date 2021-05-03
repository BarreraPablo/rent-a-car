using RentACar.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface ICarService
    {
        Task Create(Car car);
        Task Delete(long id);
        IEnumerable<Car> GetAll();
        Task Update(Car car);
    }
}