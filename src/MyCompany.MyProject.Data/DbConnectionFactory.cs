using System.Data.Common;
using System.Data.SqlClient;

namespace MyCompany.MyProject.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public DbConnection Default()
        {
            return new SqlConnection("");
        }
    }
}
