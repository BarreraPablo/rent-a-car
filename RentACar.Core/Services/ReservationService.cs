using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Reservation> GetReservations() => unitOfWork.ReservationRepository.GetAll();

        public async Task Create(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException();
            }

            reservation.validateReservation();
            reservation.calculateTotal();

            await unitOfWork.ReservationRepository.Add(reservation);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task Update(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException();
            }

            var existentReservation = await unitOfWork.ReservationRepository.GetById(reservation.Id);

            if(existentReservation == null)
            {
                throw new NullEntityException();
            }

            if(existentReservation.Status == Enumerations.ReservationStatus.Finished)
            {
                throw new BussinessException("The finished reservations cannot be modified");
            }

            reservation.validateReservation();
            reservation.calculateTotal();

            await unitOfWork.ReservationRepository.CheckAndUpdate(existentReservation, reservation);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
