using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Persistence;

namespace MyCompany.MyProject.Infrastructure
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly DefaultDbContext _appDbContext;

        public DbConnectionFactory(DefaultDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public DbConnection Default()
        {
            return _appDbContext.Database.GetDbConnection();
        }
    }
}
