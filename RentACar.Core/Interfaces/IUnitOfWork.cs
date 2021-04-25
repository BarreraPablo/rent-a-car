using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IUnitOfWork
    {
        //IBrandRepository BrandRepository { get; }

        IUserRepository UserRepository { get; }

        void Dispose();

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
