using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MyCompany.MyProject.Persistence
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
