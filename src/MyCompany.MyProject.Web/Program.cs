using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCompany.MyProject.Web.Internal;

namespace MyCompany.MyProject.Web
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(UseSecretsInDebug)
                .ConfigureServices((ctx, services) => services.Configure<AppSettings>(ctx.Configuration))
                .ConfigureLogging(b => b.Services.AddSingleton<ILoggerProvider, AppLoggerProvider>());

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 在debug编译阶段总是采用引入本地机密机制
        /// </summary>
        /// <param name="context"></param>
        /// <param name="builder"></param>
        private static void UseSecretsInDebug(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            var debug = Assembly.GetExecutingAssembly().GetCustomAttribute<DebuggableAttribute>();
            if (debug.IsJITTrackingEnabled) builder.AddUserSecrets<Startup>();
        }
    }
}
