using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class Client : BaseEntity
    {
        public Client()
        {
            Reservation = new HashSet<Reservation>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? CountryId { get; set; }
        public long DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public virtual Country Country { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }

        public int CalculateAge()
        {
            int age = 0;
            age = DateTime.Now.Year - this.Birthday.Year;
            if (DateTime.Now.DayOfYear < this.Birthday.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}
