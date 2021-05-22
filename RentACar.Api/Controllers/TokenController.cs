using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IPasswordService passwordService;
        private readonly ITokenService tokenService;

        public TokenController(IUserService userService, IPasswordService passwordHasher, ITokenService tokenService)
        {
            this.userService = userService;
            this.passwordService = passwordHasher;
            this.tokenService = tokenService;
        }


        [HttpPost]
        public async Task<IActionResult> Authentication(UserLoginReqDto login)
        {
            var validation = await IsValidUser(login);
            if (validation.Item1)
            {
                var token = tokenService.GenerateToken(validation.Item2);
                RefreshToken refreshToken = tokenService.GenerateRefreshToken(IpAddress());
                refreshToken.UserId = validation.Item2.Id;

                await tokenService.SaveRefreshToken(refreshToken);

                SetTokenCookie(refreshToken.Token);
                return Ok(new { token = token }); 
            }

            return Unauthorized();
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            UserLoginResDto tokens = await tokenService.GenerateTokenAndRefreshToken(refreshToken, IpAddress());

            if(tokens == null)
            {
                return Unauthorized();
            }

            SetTokenCookie(tokens.RefreshToken);

            return Ok(new { Token = tokens.JwtToken });
        }


        private async Task<(bool, User)> IsValidUser(UserLoginReqDto login)
        {
            var user = await userService.GetByUsername(login);
            if (user == null)
            {
                return (false, user);
            }

            bool isValid = passwordService.Check(user.Password, login.Password);

            return (isValid, user);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
