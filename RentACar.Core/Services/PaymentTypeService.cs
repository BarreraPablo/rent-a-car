using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IUnitOfWork unitOfWork;

        public PaymentTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<PaymentType> GetAll() => unitOfWork.PaymentTypeRepository.GetAll();

        public async Task Create(PaymentType paymentType)
        {
            if (paymentType == null)
            {
                throw new ArgumentNotDefinedException();
            }

            await unitOfWork.PaymentTypeRepository.Add(paymentType);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task Update(PaymentType paymentType)
        {
            if (paymentType == null)
            {
                throw new ArgumentNotDefinedException();
            }

            await unitOfWork.PaymentTypeRepository.GetAndUpdate(paymentType);
            await unitOfWork.SaveChangesAsync();
        }

    }
}
