using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;

namespace RentACar.Infrastructure.Repositories
{
    public class DocumentTypeRepository : BaseRepository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(RentACarContext context) : base(context)
        {

        }
    }
}
