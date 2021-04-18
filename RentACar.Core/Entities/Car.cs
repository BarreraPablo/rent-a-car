using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class Car
    {
        public Car()
        {
            RentTypes = new HashSet<RentType>();
        }

        public long CarId { get; set; }
        public string Model { get; set; }
        public long BrandId { get; set; }
        public byte Doors { get; set; }
        public bool AirConditioner { get; set; }
        public string Gearbox { get; set; }
        public int? Year { get; set; }
        public int? BodyTypeId { get; set; }
        public byte? QuantityAvailable { get; set; }
        public byte? Seats { get; set; }
        public bool Available { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual BodyType BodyType { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<RentType> RentTypes { get; set; }
    }
}
