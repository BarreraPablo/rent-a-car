using Microsoft.EntityFrameworkCore;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

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
    }
}
