using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class DocumentType : BaseEntity
    {
        public DocumentType()
        {
            Clients = new HashSet<Client>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
