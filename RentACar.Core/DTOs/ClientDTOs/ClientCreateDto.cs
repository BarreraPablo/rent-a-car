using RentACar.Core.CountryDTOs;
using RentACar.Core.Entities;
using System;
using System.Collections.Generic;

namespace RentACar.Core.DTOs.ClientDTOs
{
    public class ClientCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public virtual CountryReadDto Country { get; set; }
    }
}
