using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Interfaces;
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
        private readonly IPasswordService passwordService;

        public UserController(IMapper mapper, IUserService userService, IPasswordService passwordService)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.passwordService = passwordService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserCreateDto userCreateDto)
        {
            var user = mapper.Map<User>(userCreateDto);

            user.Password = passwordService.Hash(user.Password);

            await userService.RegisterUser(user);

            return NoContent();
        }

    }
}
