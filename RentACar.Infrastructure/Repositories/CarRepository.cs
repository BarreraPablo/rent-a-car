using Microsoft.EntityFrameworkCore;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
