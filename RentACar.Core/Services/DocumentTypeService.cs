using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IUnitOfWork unitOfWork;

        public DocumentTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<DocumentType> GetAll()
        {
            return this.unitOfWork.DocumentTypeRepository.GetAll();
        }

        public async Task Create(DocumentType documentType)
        {
            await this.unitOfWork.DocumentTypeRepository.Add(documentType);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task Update(DocumentType documentType)
        {
            DocumentType existentDocType = await this.unitOfWork.DocumentTypeRepository.GetById(documentType.Id);

            if(existentDocType == null)
            {
                throw new NullEntityException();
            }

            existentDocType.Name = documentType.Name;
            existentDocType.Description = documentType.Description;

            await this.unitOfWork.DocumentTypeRepository.Update(existentDocType);
            await this.unitOfWork.SaveChangesAsync();
        }
    }
}
