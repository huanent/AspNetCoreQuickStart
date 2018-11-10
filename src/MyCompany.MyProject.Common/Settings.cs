namespace MyCompany.MyProject.Common
{
    public class ConnectionStrings
    {
        /// <summary>
        /// 默认连接
        /// </summary>
        public string Default { get; set; }
    }

    public class Settings
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; }

        public string[] CorsOrigins { get; set; }
    }
}
