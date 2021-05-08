using System;

namespace RentACar.Core.DTOs.ReservationDTOs
{
    public class ReservationUpdateDto
    {
        public long Id { get; set; }
        public DateTime PickUp { get; set; }
        public DateTime PickOff { get; set; }
        public long PaymentTypeId { get; set; }
        public bool Paid { get; set; }
        public long CliendId { get; set; }
        public long CarId { get; set; }
    }
}
