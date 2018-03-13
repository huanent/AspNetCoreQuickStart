using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace FunctionalTests.Controllers
{
    public class DemoControllerTests
    {
        [Fact]
        public void RunTransactionTest()
        {
            var client = TestClientFactory.Client();
            var rsp = client.GetAsync("api/Demo/RunTransaction").Result;
            Assert.Equal(HttpStatusCode.OK, rsp.StatusCode);
        }
    }
}
