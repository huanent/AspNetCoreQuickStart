using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCompany.MyProject.Common;
using MyCompany.MyProject.Web.Application;

namespace MyCompany.MyProject.Web
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureServices((ctx, services) =>
                {
                    services.Configure<AppSettings>(ctx.Configuration);
                })
                .ConfigureLogging(builder => builder.AddFile(options =>
                {
                    options.Path = Path.Combine(AppContext.BaseDirectory, Constants.DataPath, "logs");
                }));

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
    }
}
