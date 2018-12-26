using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using SpaRouterTest;
using Xunit;

namespace IntegrationTests
{
    public class BasicTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public BasicTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/Spa200")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            Assert.Equal("Home Index", body);
        }

        [Theory]
        [InlineData("/api/index")]
        public async Task Get_EndpointsReturnApiSuccess(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            Assert.Equal("Api Get Index", body);
        }

        [Theory]
        [InlineData("/api")]
        [InlineData("/api/toto")]
        public async Task Get_EndpointsReturnFailure(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
