using RentACar.Core.Entities;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IBrandRepository BrandRepository { get; }

        IUserRepository UserRepository { get; }

        IBodyTypeRepository BodyTypeRepository { get; }

        IDocumentTypeRepository DocumentTypeRepository { get; }

        ICarRepository CarRepository { get; }

        IPaymentTypeRepository PaymentTypeRepository { get; }

        void Dispose();

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
