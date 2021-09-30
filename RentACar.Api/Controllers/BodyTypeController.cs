using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.DTOs.BodyTypeDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RentACar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BodyTypeController : ControllerBase
    {
        private readonly IBodyTypeService bodyTypeService;
        private readonly IMapper mapper;

        public BodyTypeController(IBodyTypeService bodyTypeService, IMapper mapper)
        {
            this.bodyTypeService = bodyTypeService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bodyTypes = bodyTypeService.GetAll();

            IEnumerable<BodyTypeReadDto> response = mapper.Map<IEnumerable<BodyTypeReadDto>>(bodyTypes);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BodyTypeCreateDto bodyTypeCreateDto)
        {
            BodyType bodyType = mapper.Map<BodyType>(bodyTypeCreateDto);

            await bodyTypeService.Add(bodyType);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(BodyTypeReadDto bodyTypeDto)
        {
            try
            {
                BodyType bodyType = mapper.Map<BodyType>(bodyTypeDto);

                await bodyTypeService.Update(bodyType);

                return NoContent();

            }
            catch (NullEntityException)
            {
                return NotFound();
            }
        }
    }
}
