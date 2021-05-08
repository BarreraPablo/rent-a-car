using RentACar.Core.Entities;

namespace RentACar.Core.Interfaces
{
    public interface IReservationRepository : ICreateRepository<Reservation>, IReadRepository<Reservation>, IUpdateRepository<Reservation>
    {
    }
}