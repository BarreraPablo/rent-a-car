using RentACar.Core.DTOs.CarDTOs;
using RentACar.Core.DTOs.ClientDTOs;
using RentACar.Core.DTOs.PaymentTypeDTOs;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.DTOs.ReservationDTOs
{
    public class ReservationReadDto
    {
        public long Id { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime PickUp { get; set; }
        public DateTime DropOff { get; set; }
        public ClientReadDto Client { get; set; }
        public PaymentTypeReadDto PaymentType { get; set; }
        public UserReadDto User { get; set; }
        public CarReadDto Car { get; set; }
    }
}
