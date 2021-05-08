using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class PaymentType : BaseEntity
    {
        public PaymentType()
        {
            Reservation = new HashSet<Reservation>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
