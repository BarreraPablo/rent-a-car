using RentACar.Core.Enumerations;
using RentACar.Core.Exceptions;
using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class Reservation : BaseEntity
    {
        public long ClientId { get; set; }
        public long UserId { get; set; }
        public long CarId { get; set; }
        public decimal Total { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime PickUp { get; set; }
        public DateTime DropOff { get; set; }
        public long PaymentTypeId { get; set; }
        public virtual Client Client { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual User User { get; set; }
        public virtual Car Car { get; set; }

        public int getNumberOfReservationDays()
        {
            int daysDiff = DropOff.Subtract(PickUp).Days;
            if(daysDiff <= 0)
            {
                throw new BussinessException("The Pick Up day must be greater than the Pick Off day");
            }

            return daysDiff;
        }

        public void validatePickUpDate()
        {
            if(PickUp.Date <= DateTime.Now.Date)
            {
                throw new BussinessException("The Pick Up day must be after today");
            }
        }

        public void validateStatus()
        {
            if (Status != ReservationStatus.Paid && Status != ReservationStatus.Pending)
            {
                throw new BussinessException("When initiating a reservation the status can be paid or pending");
            }
        }

        public void validateReservation()
        {
            validateStatus();
            validatePickUpDate();
        }

        public void calculateTotal()
        {
            Total = getNumberOfReservationDays() * Car.PricePerDay;
        }
    }
}
