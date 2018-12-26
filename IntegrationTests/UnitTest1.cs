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
        [InlineData("/", "Home Index")]
        [InlineData("/Index", "Home Index")]
        [InlineData("/Spa200", "Home Index")]
        [InlineData("/another/derp", "Another Derp")]
        [InlineData("/api/index", "Api Get Index")]
        [InlineData("/not/api/index", "Home Index")]
        public async Task Get_EndpointsReturnSuccess(string url, string expectedBody)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            Assert.Equal(expectedBody, body);
        }
        
        [Theory]
        [InlineData("/api")]
        [InlineData("/api/toto/2")]
        public async Task Get_EndpointsReturnFailure(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
