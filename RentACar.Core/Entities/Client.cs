using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class Client
    {
        public Client()
        {
            Rents = new HashSet<Rent>();
        }

        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CountryId { get; set; }
        public byte DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual Country Country { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
