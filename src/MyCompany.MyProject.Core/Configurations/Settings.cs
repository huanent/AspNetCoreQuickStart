using System;
using System.IO;
using MyCompany.MyProject.Core.Configurations;

namespace MyCompany.MyProject
{
    public class Settings
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; }

        /// <summary>
        /// 跨域放行站点
        /// </summary>
        public string[] CorsOrigins { get; set; }

        /// <summary>
        /// 日志路径
        /// </summary>
        public string LogPath { get; set; } = Path.Combine(AppContext.BaseDirectory, Constants.DataPath, "logs");
    }
}
