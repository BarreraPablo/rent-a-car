using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class RentType : BaseEntity
    {
        public RentType()
        {
            Rents = new HashSet<Rent>();
        }

        public decimal PricePerDay { get; set; }
        public bool CollisionDamageWaiver { get; set; }
        public bool ThirdPartyLiability { get; set; }
        public bool TheftProtection { get; set; }
        public bool Cancellable { get; set; }
        public DateTime AvailableUntil { get; set; }
        public virtual Car Car { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
