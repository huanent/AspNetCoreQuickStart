using ApplicationCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Entities;
using Infrastructure.Data;
using ApplicationCore.Exceptions;

namespace Infrastructure.RepositoriesImplment
{
    internal class DemoRepository : IDemoRepository
    {
        AppDbContext _dbContext;
        public DemoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Demo GetDemoByKey(Guid key)
        {
            var entity = _dbContext.Demo.Find(key);
            if (entity == null) throw new KeyNotFoundException();
            return entity;
        }

        public bool SaveDemo(Demo entity)
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
