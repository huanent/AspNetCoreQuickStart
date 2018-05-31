using Microsoft.AspNetCore.Mvc.Testing;
using System;
using Web;
using Xunit;

namespace IntegrationTests
{
    public class DemoTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        readonly WebApplicationFactory<Startup> _factory;

        public DemoTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Test1()
        {
            var client = _factory.CreateClient();
            string time = client.GetStringAsync("/api/demo/NowDateTime").Result;
        }
    }
}
