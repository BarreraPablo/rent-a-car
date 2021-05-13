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

        public override IEnumerable<Car> GetAll()
        {
            return entities.Include("Brand").Include("BodyType").AsEnumerable();
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
