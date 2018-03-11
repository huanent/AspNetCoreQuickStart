using ApplicationCore.Entities;
using ApplicationCore.IRepositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DemoRepository : IDemoRepository
    {
        readonly AppDbContext _appDbContext;

        public DemoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddDemoAsync(Demo demo)
        {
            _appDbContext.Add(demo);
            await _appDbContext.SaveChangesAsync();
        }



        public IEnumerable<Demo> AllDemo()
        {
            return _appDbContext.Demo.ToArray();
        }

        public IEnumerable<Demo> GetTop10Demo()
        {
            using (var connection = _appDbContext.Database.GetDbConnection())
            {
                return connection.Query<Demo>("select top (10) * from Demo");
            }
        }
    }
}
