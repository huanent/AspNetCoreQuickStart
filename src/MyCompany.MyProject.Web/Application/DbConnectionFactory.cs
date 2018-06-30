using MyCompany.MyProject.Infrastructure.Data;
using Microsoft.Extensions.Options;
using System.Data.Common;
using System.Data.SqlClient;

namespace MyCompany.MyProject.Web.Application
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        readonly ConnectionStrings _connectionStrings;

        public DbConnectionFactory(IOptions<ConnectionStrings> options)
        {
            _connectionStrings = options.Value;
        }

        public DbConnection Default()
        {
            return new SqlConnection(_connectionStrings.Default);
        }

        public DbConnection DefaultQuery()
        {
            return new SqlConnection(_connectionStrings.DefaultQuery);
        }
    }
}
