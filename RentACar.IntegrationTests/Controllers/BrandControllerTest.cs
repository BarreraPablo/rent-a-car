using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RentACar.Api;
using RentACar.Core.DTOs.BrandDTOs;
using RentACar.Core.Entities;
using RentACar.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RentACar.IntegrationTests.Helpers;


namespace RentACar.IntegrationTests.Controllers
{
    public class BrandControllerTest
    {
        private CustomWebApplicationFactory<Startup> factory;

        private HttpClient httpClient { get; set; }

        BrandCreateDto brandCreateDto = new BrandCreateDto
        {
            Name = $"Brand-Test-{DateTime.Now.Date}",
            Description = "Description-test"
        };

        BrandReadDto brandReadDto = new BrandReadDto();

        [OneTimeSetUp]
        public void SetupWebApplication()
        {
            factory = new CustomWebApplicationFactory<Startup>();
            factory.ClientOptions.BaseAddress = new Uri("https://localhost/api/brand");
            this.httpClient = factory.CreateClient();

        }

        [Test, Order(1)]
        public async Task Add_ReturnsOk()
        {
            var response = await httpClient.PostAsJsonAsync("", brandCreateDto);

            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Order(2)]
        public async Task Update_ReturnsOk()
        {
            var brands = await httpClient.GetFromJsonAsync<IEnumerable<BrandReadDto>>("");

            var brandFound = brands.Single(b => b.Name == $"Brand-Test-{DateTime.Now.Date}");
            brandReadDto.Id = brandFound.Id;
            brandReadDto.Name = $"Brand-Test-Updated-{DateTime.Now.Date}";
            brandReadDto.Description = "Description-test-updated";

            await httpClient.PutAsJsonAsync("", brandFound);
        }

        [Test, Order(3)]
        public async Task GetAll_ReturnsBrandsWithUpdatedBrand()
        {
            // GetFromJsonAsync
            // - Case insensitive deserialization
            // - Validates response has success status code
            // - Validates response include content
            // - Validates Content-Type includes content
            var response = await httpClient.GetFromJsonAsync<IEnumerable<BrandReadDto>>("");

            Assert.True(response.Count() > 0);
            Assert.NotNull(response.Single(b => b.Id == brandReadDto.Id));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            DbContextHelper.DeleteRange<Brand>(new List<Brand> { new Brand { Id = brandReadDto.Id } });

            httpClient.Dispose();
            factory.Dispose();
        }
    }
}