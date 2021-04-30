using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class BodyType : BaseEntity
    {
        public BodyType()
        {
            Cars = new HashSet<Car>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
