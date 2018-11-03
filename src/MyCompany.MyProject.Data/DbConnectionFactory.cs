using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyCompany.MyProject.Common;

namespace MyCompany.MyProject.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly AppDbContext _appDbContext;

        public DbConnectionFactory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public DbConnection Default()
        {
            return _appDbContext.Database.GetDbConnection();
        }
    }
}
