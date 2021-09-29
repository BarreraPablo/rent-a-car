using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RentACar.Api;
using RentACar.Core.DTOs.BodyTypeDTOs;
using RentACar.Core.DTOs.BrandDTOs;
using NUnit.Framework;
using RentACar.Core.Entities;
using RentACar.IntegrationTests.Helpers;

namespace RentACar.IntegrationTests.Controllers
{
    public class BodyTypeControllerTest
    {
        private CustomWebApplicationFactory<Startup> factory;
        private HttpClient httpClient { get; set; }
        BodyTypeReadDto bodyTypeReadDto = new BodyTypeReadDto();
        private readonly string guid = Guid.NewGuid().ToString().Substring(0, 10);


        [OneTimeSetUp]
        public void SetupWebApplication()
        {
            factory = new CustomWebApplicationFactory<Startup>();
            factory.ClientOptions.BaseAddress = new Uri("https://localhost/api/bodytype");
            this.httpClient = factory.CreateClient();
        }

        [Test, Order(1)]
        public async Task Add_ReturnsOk()
        {
            var bodyTypeCreateDto = new BodyTypeCreateDto()
            {
                Name = $"BodyTypeTest-{guid}",
                Description = $"DescriptionTest"
            };

            var response = await httpClient.PostAsJsonAsync("", bodyTypeCreateDto);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test, Order(2)]
        public async Task Update_ReturnsOk()
        {
            var bodyTypes = await httpClient.GetFromJsonAsync<IEnumerable<BodyTypeReadDto>>("");

            var bodyTypeToUpdate = bodyTypes.Single(b => b.Name == $"BodyTypeTest-{guid}");
            bodyTypeToUpdate.Name = $"Updated-{guid}";
            bodyTypeToUpdate.Description = $"DescriptionUpdated";

            var response = await httpClient.PutAsJsonAsync("", bodyTypeToUpdate);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            bodyTypeReadDto = bodyTypeToUpdate;
        }

        [Test, Order(3)]
        public async Task GetAll_ReturnsAllBodyTypesWithUpdatedBodyType()
        {
            var bodyTypes = await httpClient.GetFromJsonAsync<IEnumerable<BodyTypeReadDto>>("");

            // Assert
            Assert.That(bodyTypes.Any());
            bodyTypes.Single(b => b.Id == bodyTypeReadDto.Id && b.Name == bodyTypeReadDto.Name && b.Description == bodyTypeReadDto.Description);
        }

        [OneTimeTearDown]
        public void ErasesAndDisposeData()
        {
            DbContextHelper.DeleteRange(new List<BodyType> {new BodyType {Id = bodyTypeReadDto.Id}});

            httpClient.Dispose();
            factory.Dispose();
        }
    }
}
