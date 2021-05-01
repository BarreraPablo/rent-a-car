using RentACar.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IDocumentTypeService
    {
        Task Create(DocumentType documentType);
        Task Update(DocumentType id);
        IEnumerable<DocumentType> GetAll();
    }
}