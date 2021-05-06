using RentACar.Core.CountryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.DTOs.ClientDTOs
{
    public class ClientUpdateDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public CountryReadDto Country { get; set; }
        public long DocumentTypeId { get; set; }
    }
}
