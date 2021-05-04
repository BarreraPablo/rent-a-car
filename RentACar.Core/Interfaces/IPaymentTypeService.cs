using RentACar.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IPaymentTypeService
    {
        Task Create(PaymentType paymentType);
        IEnumerable<PaymentType> GetAll();
        Task Update(PaymentType paymentType);
    }
}