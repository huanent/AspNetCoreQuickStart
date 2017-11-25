using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Web;

namespace FunctionalTests
{
    public static class TestClientFactory
    {
        static HttpClient _httpClient;
        static object _locker = new object();

        public static HttpClient Client()
        {
            lock (_locker)
            {
                if (_httpClient == null)
                {
                    lock (_locker)
                    {
                        _httpClient = new TestServer(WebHost
                            .CreateDefaultBuilder()
                            .UseStartup<Startup>()
                            .UseEnvironment("Development")
                            .UseContentRoot(GetProjectPath("AspNetCoreQuickStart.sln", "src", typeof(Startup).Assembly))
                            ).CreateClient();
                    }
                }
            }

            return _httpClient;
        }

        /// <summary>
        /// 获取工程路径
        /// </summary>
        /// <param name="slnName">解决方案文件名，例test.sln</param>
        /// <param name="solutionRelativePath">如果项目与解决方案文件不在一个目录，例如src文件夹中，则传src</param>
        /// <param name="startupAssembly">程序集</param>
        /// <returns></returns>
        private static string GetProjectPath(string slnName, string solutionRelativePath, Assembly startupAssembly)
        {
            string projectName = startupAssembly.GetName().Name;
            string applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, slnName));
                if (solutionFileInfo.Exists)
                {
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
                }

                directoryInfo = directoryInfo.Parent;
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
        }
    }
}
