using RentACar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IDocumentTypeRepository : ICreateRepository<DocumentType>, IReadRepository<DocumentType>, IUpdateRepository<DocumentType>
    {
    }
}
