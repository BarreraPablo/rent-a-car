using RentACar.Core.CountryDTOs;
using RentACar.Core.DTOs.DocumentTypeDTOs;
using RentACar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.DTOs.ClientDTOs
{
    public class ClientReadDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public virtual CountryReadDto Country { get; set; }
        public virtual DocumentTypeReadDto DocumentType { get; set; }
    }
}
