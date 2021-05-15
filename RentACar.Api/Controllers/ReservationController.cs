using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.DTOs.ReservationDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Enumerations;
using RentACar.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;
        private readonly IMapper mapper;
        private readonly ICarService carService;

        public ReservationController(IMapper mapper, IReservationService reservationService, ICarService carService)
        {
            this.reservationService = reservationService;
            this.mapper = mapper;
            this.carService = carService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Reservation> reservations = reservationService.GetReservations();

            IEnumerable<ReservationReadDto> reservationReadDtos = mapper.Map<IEnumerable<ReservationReadDto>>(reservations);

            return Ok(reservationReadDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            Reservation reservation = await reservationService.GetById(id);

            ReservationReadDto reservationReadDto = mapper.Map<ReservationReadDto>(reservation);

            return Ok(reservationReadDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationCreateDto reservationCreateDto)
        {
            Reservation reservation = mapper.Map<Reservation>(reservationCreateDto);
            reservation.Status = reservationCreateDto.Paid ? ReservationStatus.Paid : ReservationStatus.Pending;
            reservation.Car = await carService.GetById(reservation.CarId);
            reservation.UserId = long.Parse(User.FindFirst("Id").Value);

            await reservationService.Create(reservation);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ReservationUpdateDto reservationCreateDto)
        {
            Reservation reservation = mapper.Map<Reservation>(reservationCreateDto);
            reservation.Status = reservationCreateDto.Paid ? ReservationStatus.Paid : ReservationStatus.Pending;
            reservation.Car = await carService.GetById(reservation.CarId);

            await reservationService.Update(reservation);

            return NoContent();
        }
    }
}
