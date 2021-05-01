using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface ICreateRepository<T> where T : class
    {
        Task Add(T entity);
    }
}
