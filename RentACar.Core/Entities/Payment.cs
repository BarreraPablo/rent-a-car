using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class Payment
    {
        public Payment()
        {
            Rents = new HashSet<Rent>();
        }

        public long PaymentId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Total { get; set; }
        public string Type { get; set; }
        public string DocumentNumber { get; set; }
        public string CreditCardExpiration { get; set; }
        public string CreditCardNumber { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }
    }
}
