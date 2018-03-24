using System;

namespace Web
{
    public class AppSettings
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 定义本程序的日志事件Id
        /// </summary>
        public int EventId { get; set; } = 64490;

        /// <summary>
        /// jwt身份验证
        /// </summary>
        public Jwt Jwt { get; set; }


    }

    public class ConnectionStrings
    {
        /// <summary>
        /// 默认连接
        /// </summary>
        public string Default { get; set; }
    }

    public class Jwt
    {
        /// <summary>
        /// 返回的http头部名称
        /// </summary>
        public string HeaderName { get; set; } = "jwt";

        /// <summary>
        /// 加密密钥
        /// </summary>
        public string Key { get; set; } = "safjniiioasfjnsklncmijaniwnk";

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Exp { get; set; } = new TimeSpan(24, 0, 0);

        /// <summary>
        /// 刷新时间
        /// </summary>
        public TimeSpan Refresh { get; set; } = new TimeSpan(0, 0, 30);
    }
}
