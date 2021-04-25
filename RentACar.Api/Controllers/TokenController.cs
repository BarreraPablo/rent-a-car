using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IUserService userService;
        private readonly IPasswordService passwordService;

        public TokenController(IConfiguration configuration, IUserService userService, IPasswordService passwordHasher)
        {
            this.Configuration = configuration;
            this.userService = userService;
            this.passwordService = passwordHasher;
        }


        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin login)
        {
            var validation = await IsValidUser(login);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { token = token }); 
            }

            return NotFound();
        }


        private async Task<(bool, User)> IsValidUser(UserLogin login)
        {
            var user = await userService.GetByUsername(login);
            var isValid = passwordService.Check(user.Password, login.Password);

            return (isValid, user);
        }

        private string GenerateToken(User user)
        {
            // Header
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            // Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            // Payload
            var payload = new JwtPayload(
                Configuration["Authentication:Issuer"],
                Configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(10)
                );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
