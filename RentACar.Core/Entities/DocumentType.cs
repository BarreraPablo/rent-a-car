using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            Clients = new HashSet<Client>();
        }

        public byte DocumentTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
