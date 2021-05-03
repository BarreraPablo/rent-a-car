using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class Car : BaseEntity
    {
        public Car()
        {
            Rent = new HashSet<Rent>();
        }

        public string Model { get; set; }
        public long BrandId { get; set; }
        public byte Doors { get; set; }
        public bool AirConditioner { get; set; }
        public string Gearbox { get; set; }
        public int? Year { get; set; }
        public long? BodyTypeId { get; set; }
        public string Image { get; set; }
        public byte? Seats { get; set; }
        public bool Available { get; set; }

        public virtual BodyType BodyType { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<Rent> Rent { get; set; }
    }
}
