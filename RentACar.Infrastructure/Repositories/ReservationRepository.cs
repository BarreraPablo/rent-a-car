using Microsoft.EntityFrameworkCore;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(RentACarContext context) : base(context)
        {

        }

        public override IEnumerable<Reservation> GetAll()
        {
            return entities.Include("Client").Include("User").Include("PaymentType").Include("Car").AsEnumerable();
        }

        public async Task<Reservation> GetByIdWith(long id, bool client, bool user, bool paymentType, bool car)
        {
            var entity = entities.AsQueryable();

            if (client)
                entity = entity.Include("Client");
            if (user)
                entity = entity.Include("User");
            if (paymentType)
                entity = entity.Include("PaymentType");
            if (car)
                entity = entity.Include("Car");

            return await entity.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
