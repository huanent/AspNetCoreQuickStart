using ApplicationCore.Dtos;
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

        public async Task AddAsync(Demo demo)
        {
            _appDbContext.Add(demo);
            await _appDbContext.SaveChangesAsync();
        }

        public IEnumerable<Demo> All()
        {
            return _appDbContext.Demo.ToArray();
        }

        public IEnumerable<Demo> GetTopRecords(int count)
        {
            using (var connection = _appDbContext.Database.GetDbConnection())
            {
                return connection.Query<Demo>("select top (@Count) * from Demo", new { Count = count });
            }
        }
    }
}
