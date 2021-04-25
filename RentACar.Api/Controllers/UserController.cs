using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IMapper mapper, IUserService userService)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserCreateDto userCreateDto)
        {
            var user = mapper.Map<User>(userCreateDto);

            await userService.RegisterUser(user);

            return Ok();
        }

    }
}
