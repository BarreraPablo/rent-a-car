using Microsoft.EntityFrameworkCore;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repositories
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(RentACarContext context) : base(context) { }

        public IEnumerable<Car> GetAllWith(bool brand, bool bodyType, bool onlyAvailable)
        {
            var entity = entities.AsQueryable();

            if (brand)
                entity = entity.Include("Brand");

            if (bodyType)
                entity = entity.Include("BodyType");

            if (onlyAvailable)
                entity = entity.Where(c => c.Available);

            return entity;
        }

        public async Task<Car> GetByIdWith(long id, bool bodyType, bool brand)
        {
            var entity = entities.AsQueryable();

            if(bodyType)
            {
                entity = entity.Include("BodyType");
            }

            if (brand)
            {
                entity = entity.Include("Brand");
            }

            return await entity.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
