using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class Country : BaseEntity
    {
        public Country()
        {
            Clients = new HashSet<Client>();
        }

        public string Name { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
