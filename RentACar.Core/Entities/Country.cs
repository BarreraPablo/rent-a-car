using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class Country
    {
        public Country()
        {
            Clients = new HashSet<Client>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
