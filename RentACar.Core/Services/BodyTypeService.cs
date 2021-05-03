using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class BodyTypeService : IBodyTypeService
    {
        private readonly IUnitOfWork unitOfWork;

        public BodyTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<BodyType> GetAll() => unitOfWork.BodyTypeRepository.GetAll();
        public async Task Update(BodyType bodyType)
        {

            await unitOfWork.BodyTypeRepository.GetAndUpdate(bodyType);
            unitOfWork.SaveChanges();
        }

        public async Task Add(BodyType bodyType)
        {
            if (bodyType == null)
            {
                throw new ArgumentNotDefinedException();
            }

            await unitOfWork.BodyTypeRepository.Add(bodyType);
            unitOfWork.SaveChanges();
        }

    }
}
