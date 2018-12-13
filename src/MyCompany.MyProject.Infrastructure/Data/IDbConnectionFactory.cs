using System.Data.Common;

namespace MyCompany.MyProject.Infrastructure
{
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// 默认连接
        /// </summary>
        /// <returns></returns>
        DbConnection Default();
    }
}
