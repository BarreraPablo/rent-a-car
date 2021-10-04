using NUnit.Framework;
using RentACar.Api;
using System.Net.Http;

namespace RentACar.IntegrationTests.Helpers
{
    public class BaseIntegrationTest
    {
        protected CustomWebApplicationFactory<Startup> factory;
        protected HttpClient httpClient { get; set; }

        [OneTimeSetUp]
        public void SetupWebApplication()
        {
            factory = new CustomWebApplicationFactory<Startup>();
            this.httpClient = factory.CreateClient();
        }
    }
}
