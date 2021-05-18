using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.CountryDTOs;
using RentACar.Core.DTOs.ClientDTOs;
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
    public class ClientController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IClientService clientService;
        private readonly ICountryService countryService;

        public ClientController(IMapper mapper, IClientService clientService, ICountryService countryService)
        {
            this.mapper = mapper;
            this.clientService = clientService;
            this.countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Client> clients = clientService.GetClients();

            IEnumerable<ClientReadDto> clientsRead = mapper.Map<IEnumerable<ClientReadDto>>(clients);

            return Ok(clientsRead);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            Client client = await clientService.GetById(id);

            ClientReadDto clientReadDto = mapper.Map<ClientReadDto>(client);

            return Ok(clientReadDto);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterClient(ClientCreateDto clientCreateDto)
        {
            Client client = mapper.Map<Client>(clientCreateDto);

            if(client.CountryId == 0)
            {
                await countryService.Create(client.Country);
                client.CountryId = client.Country.Id;
            }
            client.Country = null;

            await clientService.Create(client);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientUpdateDto clientReadDto)
        {
            Client client = mapper.Map<Client>(clientReadDto);

            if (client.CountryId == 0)
            {
                await countryService.Create(client.Country);
                client.CountryId = client.Country.Id;
            }
            client.Country = null;

            await clientService.Update(client);

            return NoContent();
        }
    }
}
