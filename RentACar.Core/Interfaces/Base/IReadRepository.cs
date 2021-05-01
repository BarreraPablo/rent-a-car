using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IReadRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(long id);
    }
}
