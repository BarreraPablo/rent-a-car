using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork unitOfWork;

        public IConfiguration Configuration { get; }

        public TokenService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.Configuration = configuration;
        }

        public async Task<UserLoginResDto> GetAuthTokens(User user, string ipAddress)
        {
            var token = GenerateToken(user);

            RefreshToken refreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.UserId = user.Id;

            await unitOfWork.RefreshTokenRepository.Add(refreshToken);
            unitOfWork.SaveChanges();

            return new UserLoginResDto { JwtToken = token, RefreshToken = refreshToken.Token };

        }

        public async Task<UserLoginResDto> ProcessRefreshToken(string token, string ipAddress)
        {
            if (String.IsNullOrWhiteSpace(token) || String.IsNullOrWhiteSpace(ipAddress))
            {
                throw new ArgumentNotDefinedException();
            }

            RefreshToken refreshToken = await unitOfWork.RefreshTokenRepository.GetByToken(token); 

            User user = await unitOfWork.UserRepository.GetById(refreshToken.UserId);

            if (!refreshToken.IsActive) return null;

            var newRefreshToken = GenerateRefreshToken(ipAddress);
            newRefreshToken.UserId = user.Id;

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            await unitOfWork.RefreshTokenRepository.Add(newRefreshToken);
            unitOfWork.SaveChanges();

            var jwtToken = GenerateToken(user);

            return new UserLoginResDto { JwtToken = jwtToken, RefreshToken = newRefreshToken.Token };
        }


        private string GenerateToken(User user)
        {
            if (user == null)
            {
                throw new ArgumentNotDefinedException();
            }

            // Header
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthOptions:SecretKey"]));
            var signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            // Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.EmailAddress ?? ""),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            // Payload
            var payload = new JwtPayload(
                Configuration["AuthOptions:Issuer"],
                Configuration["AuthOptions:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(1)
                );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

    }
}
