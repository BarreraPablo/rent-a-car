using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.CountryDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICountryService countryService;

        public CountryController(IMapper mapper, ICountryService countryService)
        {
            this.mapper = mapper;
            this.countryService = countryService;
        }


        [HttpGet]
        public IActionResult GetCountries()
        {
            IEnumerable<Country> countries = countryService.GetAll();

            IEnumerable<CountryReadDto> countryReadDtos = mapper.Map<IEnumerable<CountryReadDto>>(countries);

            return Ok(countryReadDtos);
        }

    }
}
