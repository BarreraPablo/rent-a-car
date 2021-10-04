using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using NUnit.Framework;
using RentACar.Api;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;
using RentACar.IntegrationTests.Helpers;

namespace RentACar.IntegrationTests.Controllers
{
    public class TokenControllerTest : BaseIntegrationTest
    {
        public string userName = "IntegrationTestUser";
        public string password = "TestPsw1.";

        [OneTimeSetUp]
        public void SetupWebApplication()
        {
            httpClient.BaseAddress = new Uri("https://localhost/api/");
        }


        [Test, Order(1)]
        public async Task Register_ReturnsNoContent()
        {
            var userCreateDto = new UserCreateDto
            {
                Username = this.userName,
                Password = this.password,
                EmailAddress = "email@contact.com"
            };

            var response = await httpClient.PostAsJsonAsync("user", userCreateDto);

            Assert.AreEqual(HttpStatusCode.NoContent,response.StatusCode);
        }

        [Test, Order(2)]
        public async Task Authentication_BadUsername_ReturnsForbid()
        {
            var userLoginReqDto = new UserLoginReqDto
            {
                Username = "BadUsername",
                Password = this.password
            };

            var response = await httpClient.PostAsJsonAsync("token", userLoginReqDto);

            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Test, Order(3)]
        public async Task Authentication_BadPassword_ReturnsForbid()
        {
            var userLoginReqDto = new UserLoginReqDto
            {
                Username = this.userName,
                Password = "badPsw"
            };

            var response = await httpClient.PostAsJsonAsync("token", userLoginReqDto);

            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }


        [Test, Order(4)]
        public async Task Authentication_ReturnsJwt()
        {
            var userLoginReqDto = new UserLoginReqDto
            {
                Username = this.userName,
                Password = this.password
            };

            var response = await httpClient.PostAsJsonAsync("token", userLoginReqDto);
            var responseBody = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            var cookies = response.Headers.GetValues("Set-Cookie").First();


            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(responseBody.Count, Is.EqualTo(1));
            Assert.That(responseBody["token"].Length, Is.GreaterThan(10));
            Assert.IsTrue(cookies.Contains("refreshToken"));
        }

        /// <summary>
        /// This tests the RefreshToken method with the cookies previously taken from the previous test (it's automatic)
        /// </summary>
        /// <returns></returns>
        [Test, Order(5)]
        public async Task RefreshToken_ReturnsOk()
        {
            var response = await httpClient.PostAsync("token/refreshToken", null);
            var responseBody = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(responseBody.Count, Is.EqualTo(1));
            Assert.That(responseBody["token"].Length, Is.GreaterThan(10));
        }

        /// <summary>
        /// Re-test the method with the refreshToken got from the previous test
        /// </summary>
        /// <returns></returns>
        [Test, Order(6)]
        public async Task RefreshToken_Retry_ReturnsOk()
        {
            var response = await httpClient.PostAsync("token/refreshToken", null);
            var responseBody = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(responseBody.Count, Is.EqualTo(1));
            Assert.That(responseBody["token"].Length, Is.GreaterThan(10));
        }

        [OneTimeTearDown]
        public async Task EraseAll()
        {
            using (var context = DbContextHelper.GetRentACarContext())
            {
                var user = context.Users.Single(u => u.Username == this.userName);
                context.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
