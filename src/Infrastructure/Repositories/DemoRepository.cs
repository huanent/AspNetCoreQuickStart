using ApplicationCore.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class DemoRepository : IDemoRepository
    {
        readonly AppDbContext _appDbContext;
        public DemoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Demo> AllDemo()
        {
            return _appDbContext.Demo.ToArray();
        }
    }
}
