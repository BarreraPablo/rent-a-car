using System.ComponentModel.DataAnnotations;

namespace RentACar.Core.DTOs.CarDTOs
{
    public class CarCreateDto
    {
        public string Model { get; set; }
        public long BrandId { get; set; }
        public byte Doors { get; set; }
        public bool AirConditioner { get; set; }
        public decimal PricePerDay { get; set; }

        [RegularExpression("Manual|Automatic", ErrorMessage = "Possibles values of Gearbox are 'Manual' and 'Automatic'")]
        public string Gearbox { get; set; }
        public int? Year { get; set; }
        public long? BodyTypeId { get; set; }
        public byte? Seats { get; set; }
    }
}
