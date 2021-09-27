using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.DTOs.BrandDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RentACar.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;
        private readonly IMapper mapper;

        public BrandController(IBrandService brandService, IMapper mapper)
        {
            this.brandService = brandService;
            this.mapper = mapper;

        }
        
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<BrandReadDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetAll()
        {
            var brands = brandService.GetAll();

            var brandsReadDto = mapper.Map<IEnumerable<BrandReadDto>>(brands);

            return Ok(brandsReadDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BrandCreateDto brandCreateDto)
        {

            var brand = mapper.Map<Brand>(brandCreateDto);

            await brandService.Add(brand);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(BrandReadDto brandCreateDto)
        {
            var brand = mapper.Map<Brand>(brandCreateDto);

            await brandService.Update(brand);

            return NoContent();
        }
    }
}
