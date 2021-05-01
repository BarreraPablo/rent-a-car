using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class Rent : BaseEntity
    {
        public long ClientId { get; set; }
        public long UserId { get; set; }
        public long RentTypeId { get; set; }
        public decimal Total { get; set; }
        public DateTime PickUp { get; set; }
        public DateTime PickOff { get; set; }
        public long PaymentTypeId { get; set; }
        public virtual Client Client { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual User User { get; set; }
    }
}
