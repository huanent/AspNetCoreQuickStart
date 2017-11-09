using ApplicationCore.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DemoRepository : IDemoRepository
    {
        readonly AppDbContext _appDbContext;
        public DemoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async System.Threading.Tasks.Task AddDemoAsync(Demo demo)
        {
            _appDbContext.Add(demo);
            await _appDbContext.SaveChangesAsync();
        }

        public IEnumerable<Demo> AllDemo()
        {

            return _appDbContext.Demo.ToArray();
        }
    }
}
