using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IUpdateRepository<T> where T : class
    {
        Task Update(T entity);
        Task GetAndUpdate(T entity);
        Task CheckAndUpdate(T oldEntity, T updatedEntity);
    }
}
