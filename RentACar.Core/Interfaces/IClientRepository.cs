using RentACar.Core.Entities;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IClientRepository : ICreateRepository<Client>, IUpdateRepository<Client>, IReadRepository<Client>
    {
        Task<Client> GetByDocumentNumber(string documentNumber);
        Task<Client> GetByIdWith(long id, bool documentType, bool country);
    }
}
