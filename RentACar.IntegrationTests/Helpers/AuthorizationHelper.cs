using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Enumerations;
using RentACar.Infrastructure.Services;

namespace RentACar.IntegrationTests.Helpers
{
    public static class AuthorizationHelper
    {
        private static string _jwt;

        public static string GetJwt()
        {
            if (!String.IsNullOrEmpty(_jwt))
            {
                return _jwt;
            }

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true)
                .AddEnvironmentVariables()
                .Build();


            var tokenService = new TokenService(null, configuration);

            var user = new User()
            {
                Username = "IntegrationTesting",
                Id = 1L,
                EmailAddress = "IntegrationTesting@gmail.com",
                Role = RoleType.Administrator
            };

            return tokenService.GenerateToken(user);
        }

        public static void ConfigureJwt(this HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", GetJwt());
        }
    }
}
