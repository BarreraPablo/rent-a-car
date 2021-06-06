using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Interfaces;
using System;
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
        private readonly ICryptographyService cryptographyService;
        private readonly IEmailService emailService;

        public UserController(IMapper mapper, IUserService userService, IPasswordService passwordService, ICryptographyService cryptographyService, IEmailService emailService)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.passwordService = passwordService;
            this.cryptographyService = cryptographyService;
            this.emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserCreateDto userCreateDto)
        {
            var user = mapper.Map<User>(userCreateDto);

            user.Password = passwordService.Hash(user.Password);

            await userService.RegisterUser(user);

            return NoContent();
        }

        [HttpPost("StartPasswordRecovery")]
        public async Task<IActionResult> StartPasswordRecovery(UserRecoverRequestDto userRecoverRequestDto)
        {
            try
            {
                string tokenRecovery = cryptographyService.GetSha256(Guid.NewGuid().ToString());

                await userService.SetTokenRecovery(userRecoverRequestDto.EmailAddress, tokenRecovery);

                emailService.SendPasswordRecoveryToken(tokenRecovery, userRecoverRequestDto.EmailAddress);

                return NoContent();
            }
            catch (UserNotFoundException)
            {
                return NoContent();
            }
        }

        [HttpPost("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword(UserPasswordRecoveryDto userPasswordRecoveryDto)
        {
            try
            {
                string password = passwordService.Hash(userPasswordRecoveryDto.Password);

                await userService.UpdatePasswordWithRecoveryToken(userPasswordRecoveryDto.RecoveryToken, password);

                return NoContent();
            }
            catch (ExpiredRecoveryTokenException)
            {
                return BadRequest(new { RecoveryToken = "The recovery token has already expired" });
            }
        }
    }
}
