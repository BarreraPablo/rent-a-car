﻿using RentACar.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IReservationService
    {
        Task Create(Reservation reservation);
        IEnumerable<Reservation> GetReservations();
        Task Update(Reservation reservation);
    }
}