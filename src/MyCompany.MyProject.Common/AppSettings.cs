namespace MyCompany.MyProject.Common
{
    public class AppSettings
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; }

        public string[] CorsOrigins { get; set; }

        /// <summary>
        /// 定义本程序的日志事件Id
        /// </summary>
        public int EventId { get; set; }
    }

    public class ConnectionStrings
    {
        /// <summary>
        /// 默认连接
        /// </summary>
        public string Default { get; set; }
    }
}
