using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using Web;
using Web.ViewModels;

namespace FunctionalTests
{
    public class TestBase
    {
        protected HttpClient _httpClient;

        public TestBase()
        {
            var webBuilder = WebHost.CreateDefaultBuilder()
                                    .UseContentRoot(@"C:\Users\huanent\source\repos\AspNetCoreQuickStart\src\Web")
                                    .UseStartup<Startup>()
                                    .UseEnvironment("Development");
            var server = new TestServer(webBuilder);
            _httpClient = server.CreateClient();


        }

        public string GetToken()
        {
            var loginViewModel = new LoginViewModel
            {
                Name = "huanent",
                Pwd = "huanenthuanent"
            };
            var content = new StringContent(JsonConvert.SerializeObject(loginViewModel), Encoding.Default, "application/json");
            return _httpClient.PostAsync("api/User/login", content).Result.Content.ReadAsStringAsync().Result;
        }
    }
}
