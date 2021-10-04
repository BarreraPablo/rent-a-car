using NUnit.Framework;
using RentACar.Core.DTOs.BodyTypeDTOs;
using RentACar.Core.Entities;
using RentACar.IntegrationTests.Helpers;
using RentACar.IntegrationTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RentACar.IntegrationTests.Controllers
{
    public class BodyTypeControllerTest : BaseIntegrationTest
    {
        BodyTypeReadDto bodyTypeReadDto = new BodyTypeReadDto();
        private readonly string guid = Guid.NewGuid().ToString().Substring(0, 10);


        [OneTimeSetUp]
        public void SetupWebApplication()
        {
            httpClient.BaseAddress = new Uri("https://localhost/api/bodytype");
        }

        [Test, Order(-1)]
        public async Task Add_ReturnsUnauthorized()
        {
            var bodyTypeCreateDto = new BodyTypeCreateDto()
            {
                Name = $"BodyTypeTest-{guid}",
                Description = $"DescriptionTest"
            };

            var response = await httpClient.PostAsJsonAsync("", bodyTypeCreateDto);

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test, Order(-1)]
        public async Task Update_ReturnsUnauthorized()
        {

            var bodyTypeToUpdate = new BodyTypeReadDto();
            bodyTypeToUpdate.Id = 1;
            bodyTypeToUpdate.Name = $"Updated-{guid}";
            bodyTypeToUpdate.Description = $"DescriptionUpdated";

            var response = await httpClient.PutAsJsonAsync("", bodyTypeToUpdate);

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test, Order(-1)]
        public async Task GetAll_ReturnsUnauthorized()
        {
            var response = await httpClient.GetAsync("");

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestCaseSource("GetInvalidBodyTypeCreateDtoAndErrorValidator"), Order(0)]
        public async Task Add_ReturnsBadRequest(BodyTypeCreateDto bodyTypeCreateDto,Action<KeyValuePair<string, string[]>> validator)
        { 
            httpClient.ConfigureJwt();

            var response = await httpClient.PostAsJsonAsync("", bodyTypeCreateDto);

            var validationDetails = await response.Content.ReadFromJsonAsync<Dictionary<string, string[]>>();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            validationDetails.ToList().ForEach(validator);
        }

        [TestCaseSource("GetInvalidBodyTypeReadDtoAndErrorValidator"), Order(0)]
        public async Task Update_ReturnsBadRequest(BodyTypeReadDto bodyTypeCreateDto, Action<KeyValuePair<string, string[]>> validator)
        { 
            httpClient.ConfigureJwt();

            var response = await httpClient.PutAsJsonAsync("", bodyTypeCreateDto);

            var validationDetails = await response.Content.ReadFromJsonAsync<Dictionary<string, string[]>>();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            validationDetails.ToList().ForEach(validator);
        }

        [Test, Order(1)]
        public async Task Add_ReturnsOk()
        {
            var bodyTypeCreateDto = new BodyTypeCreateDto()
            {
                Name = $"BodyTypeTest-{guid}",
                Description = $"DescriptionTest"
            };

            httpClient.ConfigureJwt();

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

            httpClient.ConfigureJwt();

            var response = await httpClient.PutAsJsonAsync("", bodyTypeToUpdate);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            bodyTypeReadDto = bodyTypeToUpdate;
        }

        [Test, Order(3)]
        public async Task GetAll_ReturnsAllBodyTypesWithUpdatedBodyType()
        {
            httpClient.ConfigureJwt();

            var bodyTypes = await httpClient.GetFromJsonAsync<IEnumerable<BodyTypeReadDto>>("");

            // Assert
            Assert.That(bodyTypes.Any());
            bodyTypes.Single(b => b.Id == bodyTypeReadDto.Id && b.Name == bodyTypeReadDto.Name && b.Description == bodyTypeReadDto.Description);
        }

        [OneTimeTearDown]
        public void ErasesAndDisposeData()
        {
            DbContextHelper.DeleteRange(new List<BodyType> {new BodyType {Id = bodyTypeReadDto.Id.Value}});

            httpClient.Dispose();
            factory.Dispose();
        }

        public static IEnumerable<object[]> GetInvalidBodyTypeCreateDtoAndErrorValidator()
        {
            var testData = new List<object[]>
            {
                new object[]
                {
                    CreateValidBodyTypeCreateDto().CloneWith(x => x.Name = new String('-', 30)),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.AreEqual(kvp.Key, "name");
                        Assert.That(kvp.Value.Length, Is.EqualTo(1));
                        Assert.AreEqual("The name must be shorter than 26 characters", kvp.Value[0]);
                    })
                },
                new object[]
                {
                    CreateValidBodyTypeCreateDto().CloneWith(x => x.Name = "a"),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.AreEqual(kvp.Key, "name");
                        Assert.That(kvp.Value.Length, Is.EqualTo(1));
                        Assert.AreEqual("The name must be longer than 2 characters", kvp.Value[0]);
                    })
                },
                new object[]
                {
                    CreateValidBodyTypeCreateDto().CloneWith(x => x.Name = null),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.AreEqual(kvp.Key, "name");
                        Assert.That(kvp.Value.Length, Is.EqualTo(1));
                        Assert.AreEqual("The Name field is required.", kvp.Value[0]);
                    })
                },
                new object[]
                {
                    CreateValidBodyTypeCreateDto().CloneWith(x => x.Description = new String('-', 201)),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.AreEqual(kvp.Key, "description");
                        Assert.That(kvp.Value.Length, Is.EqualTo(1));
                        Assert.AreEqual("The description must be shorter than 200 characters", kvp.Value[0]);
                    })
                },
                new object[]
                {
                    CreateValidBodyTypeCreateDto().CloneWith(x => x.Description = "-"),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.AreEqual(kvp.Key, "description");
                        Assert.That(kvp.Value.Length, Is.EqualTo(1));
                        Assert.AreEqual("The description must be longer than 2 characters", kvp.Value[0]);
                    })
                }
            };

            return testData;
        }

        public static IEnumerable<object[]> GetInvalidBodyTypeReadDtoAndErrorValidator()
        {
            var testData = new List<object[]>
            {
                new object[]
                {
                    CreateValidTestBodyTypeReadDto().CloneWith(x => x.Id = null),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.AreEqual(kvp.Key, "id");
                        Assert.That(kvp.Value.Length, Is.EqualTo(1));
                        Assert.AreEqual("Invalid Id", kvp.Value[0]);
                    })
                },
                new object[]
                {
                    CreateValidTestBodyTypeReadDto().CloneWith(x => x.Name = new String('-', 30)),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.AreEqual(kvp.Key, "name");
                        Assert.That(kvp.Value.Length, Is.EqualTo(1));
                        Assert.AreEqual("The name must be shorter than 26 characters", kvp.Value[0]);
                    })
                },
                new object[]
                {
                    CreateValidTestBodyTypeReadDto().CloneWith(x => x.Name = "a"),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.AreEqual(kvp.Key, "name");
                        Assert.That(kvp.Value.Length, Is.EqualTo(1));
                        Assert.AreEqual("The name must be longer than 2 characters", kvp.Value[0]);
                    })
                },
                new object[]
                {
                    CreateValidTestBodyTypeReadDto().CloneWith(x => x.Name = null),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.AreEqual(kvp.Key, "name");
                        Assert.That(kvp.Value.Length, Is.EqualTo(1));
                        Assert.AreEqual("Invalid name", kvp.Value[0]);
                    })
                },
                new object[]
                {
                    CreateValidTestBodyTypeReadDto().CloneWith(x => x.Description = new String('-', 201)),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.AreEqual(kvp.Key, "description");
                        Assert.That(kvp.Value.Length, Is.EqualTo(1));
                        Assert.AreEqual("The description must be shorter than 200 characters", kvp.Value[0]);
                    })
                },
                new object[]
                {
                    CreateValidTestBodyTypeReadDto().CloneWith(x => x.Description = "-"),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.AreEqual(kvp.Key, "description");
                        Assert.That(kvp.Value.Length, Is.EqualTo(1));
                        Assert.AreEqual("The description must be longer than 2 characters", kvp.Value[0]);
                    })
                }
            };

            return testData;
        }

        public static TestBodyTypeCreateDto CreateValidBodyTypeCreateDto()
        {
            return new TestBodyTypeCreateDto()
            {
                Name = "Sedan",
                Description = "Description"
            };
        }

        public static TestBodyTypeReadDto CreateValidTestBodyTypeReadDto()
        {
            return new TestBodyTypeReadDto()
            {
                Id = 40,
                Name = "Sedan",
                Description = "Description"
            };
        }
    }
}
