using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCompany.MyProject.Web.Application;
using System;
using System.IO;

namespace MyCompany.MyProject.Web
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(builder => builder.AddFile())
                .ConfigureAppConfiguration(builder =>
                {
                    string path = Path.Combine(AppContext.BaseDirectory, Constants.DataPath);
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    builder.SetBasePath(path);
                });

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
    }
}