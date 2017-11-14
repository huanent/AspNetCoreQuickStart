using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Web;
using Web.ViewModels;
using Xunit;

namespace FunctionalTests
{
    public class UserControllerTest : TestBase
    {
        [Fact]
        public void Get()
        {
            string token = GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            string result = _httpClient.GetStringAsync("api/user").Result;
        }
    }
}
