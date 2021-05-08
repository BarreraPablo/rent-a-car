using System;

namespace RentACar.Core.DTOs.ReservationDTOs
{
    public class ReservationCreateDto
    {
        public long ClientId { get; set; }
        public long CarId { get; set; }
        public bool Paid { get; set; }
        public DateTime PickUp { get; set; }
        public DateTime PickOff { get; set; }
        public long PaymentTypeId { get; set; }
    }
}
