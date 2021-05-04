using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.DTOs.PaymentTypeDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPaymentTypeService paymentTypeService;

        public PaymentTypeController(IMapper mapper, IPaymentTypeService paymentTypeService)
        {
            this.mapper = mapper;
            this.paymentTypeService = paymentTypeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var paymentsTypes = paymentTypeService.GetAll();

            IEnumerable<PaymentTypeReadDto> paymentsTypesRead = mapper.Map<IEnumerable<PaymentTypeReadDto>>(paymentsTypes);

            return Ok(paymentsTypesRead);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentTypeCreateDto paymentTypeCreateDto)
        {
            var paymentsType = mapper.Map<PaymentType>(paymentTypeCreateDto);

            await paymentTypeService.Create(paymentsType);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentTypeReadDto paymentTypeReadDto)
        {
            var paymentsType = mapper.Map<PaymentType>(paymentTypeReadDto);

            await paymentTypeService.Update(paymentsType);

            return NoContent();
        }
    }
}
