using Infrastructure;
using Infrastructure.Implements;
using Microsoft.Extensions.Options;
using System.Data.Common;
using System.Data.SqlClient;

namespace Web.Application
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
    }
}
