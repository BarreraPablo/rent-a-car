using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.DTOs.CarDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICarService carService;
        private readonly IFileService fileService;

        public CarController(IMapper mapper, ICarService carService, IFileService fileService)
        {
            this.mapper = mapper;
            this.carService = carService;
            this.fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                string baseUrl = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host, Request.PathBase);

                var cars = carService.GetAll();

                fileService.PrependUrl(cars, baseUrl);

                var carsReadDto = mapper.Map<IEnumerable<CarReadDto>>(cars);
                return Ok(carsReadDto);
            }
            catch (NullEntityException)
            {

                return NotFound();
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CarCreateDto carCreateDto, IFormFile image)
        {
            Car car = mapper.Map<Car>(carCreateDto);

            if (image != null)
            {
                string imageName = await fileService.SaveImage(image);
                car.Image = imageName;
            }

            await carService.Create(car);

            return NoContent();
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateCar([FromForm] CarUpdateDto carUpdateDto, IFormFile image)
        {
            Car car = mapper.Map<Car>(carUpdateDto);

            if (image != null)
            {
                string imageName = await fileService.SaveImage(image);
                car.Image = imageName;
            }

            await carService.Update(car);

            return NoContent();
        }
    }
}
