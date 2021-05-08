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
        public DateTime PickOff { get; set; }
        public long PaymentTypeId { get; set; }
        public virtual Client Client { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual User User { get; set; }
        public virtual Car Car { get; set; }
    }
}
