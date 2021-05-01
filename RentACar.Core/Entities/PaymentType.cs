using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class PaymentType : BaseEntity
    {
        public PaymentType()
        {
            Rents = new HashSet<Rent>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
