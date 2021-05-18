using Microsoft.EntityFrameworkCore;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(RentACarContext context) : base(context)
        {

        }

        public override IEnumerable<Client> GetAll()
        {
            return entities.Include("DocumentType").Include("Country").AsEnumerable();
        }

        public async Task<Client> GetByIdWith(long id, bool documentType, bool country)
        {
            var entity = entities.AsQueryable();

            if (documentType)
                entity = entity.Include("DocumentType");

            if (country)
                entity = entity.Include("Country");

            return await entity.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client> GetByDocumentNumber(string documentNumber)
        {
            return await entities.FirstOrDefaultAsync(c => c.DocumentNumber == documentNumber);
        }
    }
}
