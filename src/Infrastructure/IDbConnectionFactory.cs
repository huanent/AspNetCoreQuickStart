using System.Data.Common;

namespace Infrastructure
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
