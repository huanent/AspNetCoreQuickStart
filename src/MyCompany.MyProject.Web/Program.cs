using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCompany.MyProject.Web.Internal;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Diagnostics;

namespace MyCompany.MyProject.Web
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(b =>
                {
                    var debug = Assembly.GetExecutingAssembly().GetCustomAttribute<DebuggableAttribute>();
                    if (debug.IsJITTrackingEnabled) b.AddUserSecrets<Startup>();
                })
                .ConfigureServices((ctx, services) => services.Configure<AppSettings>(ctx.Configuration))
                .ConfigureLogging(b =>
                    b.AddFile(options =>
                        options.Path = Path.Combine(AppContext.BaseDirectory, Constants.DataPath, "logs")
                    )
                );

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
    }
}
