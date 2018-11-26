using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MyCompany.MyProject.Persistence.Internal
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
