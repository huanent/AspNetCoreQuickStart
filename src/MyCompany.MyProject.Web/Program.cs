using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#if DEBUG

using Microsoft.Extensions.Configuration;

#endif

namespace MyCompany.MyProject.Web
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
#if DEBUG
                .ConfigureAppConfiguration(b => b.AddUserSecrets<Startup>())
#endif
                .ConfigureServices((ctx, services) => services.Configure<Settings>(ctx.Configuration))
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
