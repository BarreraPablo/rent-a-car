using RentACar.Core.Enumerations;
using System;
using System.Collections.Generic;

#nullable disable

namespace RentACar.Core.Entities
{
    public partial class User : BaseEntity
    {
        public User()
        {
            Rents = new HashSet<Rent>();
        }

        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
    }
}
