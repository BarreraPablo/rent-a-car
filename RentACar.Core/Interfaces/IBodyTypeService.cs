using RentACar.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IBodyTypeService
    {
        Task Add(BodyType bodyType);
        Task Update(BodyType bodyType);
        IEnumerable<BodyType> GetAll();
    }
}