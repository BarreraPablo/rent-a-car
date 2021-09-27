using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using RentACar.Api;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentACar.IntegrationTests
{
    public class HealthChecksTest
    {
        private HttpClient httpClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            var factory = new WebApplicationFactory<Startup>();
            this.httpClient = factory.CreateClient();
        }

        [Test]
        public async Task HealthCheck__ReturnsOk()
        {
            var response = await httpClient.GetAsync("/healthcheck");

            response.EnsureSuccessStatusCode();
        }
    }
}
