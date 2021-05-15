using RentACar.Core.Entities;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IReservationRepository : ICreateRepository<Reservation>, IReadRepository<Reservation>, IUpdateRepository<Reservation>
    {
        Task<Reservation> GetByIdWith(long id, bool client, bool user, bool paymentType, bool car);
    }
}