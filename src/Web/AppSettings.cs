using Microsoft.IdentityModel.Tokens;
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
        public string Key { get; set; } = "zasjdncfslxoanaslkx";

        /// <summary>
        /// 安全算法
        /// </summary>
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256;

        /// <summary>
        /// 在什么时间之前不可用
        /// </summary>
        public TimeSpan NotBefore { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Exp { get; set; } = new TimeSpan(24, 0, 0);

        /// <summary>
        /// 刷新时间
        /// </summary>
        public TimeSpan Refresh { get; set; } = new TimeSpan(0, 0, 30);

        /// <summary>
        /// 自动续期（类似cookie方式，如果用户在不停刷新网页，则令牌过期时间始终为最后一次刷新网页时间加Exp时间）
        /// </summary>
        public bool AutoRefresh { get; set; } = true;
    }
}
