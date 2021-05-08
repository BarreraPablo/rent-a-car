using RentACar.Core.DTOs.BodyTypeDTOs;
using RentACar.Core.DTOs.BrandDTOs;
using RentACar.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.DTOs.CarDTOs
{
    public class CarReadDto
    {
        public long Id { get; set; }
        public string Model { get; set; }
        public BrandReadDto Brand { get; set; }
        public byte Doors { get; set; }
        public bool AirConditioner { get; set; }
        public decimal PricePerDay { get; set; }
        public string Gearbox { get; set; }
        public string Image { get; set; }
        public int? Year { get; set; }
        public BodyTypeReadDto BodyType { get; set; }
        public byte? Seats { get; set; }
        public bool Available { get; set; }
    }
}
