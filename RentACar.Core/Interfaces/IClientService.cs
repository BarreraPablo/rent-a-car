using RentACar.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IClientService
    {
        Task Create(Client client);
        IEnumerable<Client> GetClients();

        Task Update(Client client);
    }
}