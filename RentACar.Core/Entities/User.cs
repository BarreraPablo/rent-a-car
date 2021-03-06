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
            Reservation = new HashSet<Reservation>();
            RefreshToken = new HashSet<RefreshToken>();
        }

        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string TokenRecovery { get; set; }
        public RoleType Role { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
