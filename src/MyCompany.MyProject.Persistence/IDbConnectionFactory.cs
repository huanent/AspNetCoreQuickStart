using System.Data.Common;

namespace MyCompany.MyProject.Persistence
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
