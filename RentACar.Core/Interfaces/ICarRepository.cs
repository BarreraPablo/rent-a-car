using RentACar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface ICarRepository : ICreateRepository<Car>, IUpdateRepository<Car>, IReadRepository<Car>
    {
        IEnumerable<Car> GetAllWith(bool brand, bool bodyType, bool onlyAvailable);
        Task<Car> GetByIdWith(long id, bool bodyType, bool brand);
    }
}
