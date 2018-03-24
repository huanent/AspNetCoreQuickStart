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
        public int EventId { get; set; }

        /// <summary>
        /// jwtToken密钥
        /// </summary>
        public string JwtKey { get; set; }
    }

    public class ConnectionStrings
    {
        /// <summary>
        /// 默认连接
        /// </summary>
        public string Default { get; set; }
    }
}
