using System;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Core.DTOs.ReservationDTOs
{
    public class ReservationCreateDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "The specified client is wrong")]
        public long ClientId { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "The specified car is wrong")]
        public long CarId { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "It's necessary to specify if the reservation is paid or not")]
        public bool Paid { get; set; }

        [Required]
        public DateTime PickUp { get; set; }

        [Required]
        public DateTime DropOff { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "The specified payment type is wrong")]
        public long PaymentTypeId { get; set; }
    }
}
