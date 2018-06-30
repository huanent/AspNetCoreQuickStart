using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Common;
using ApplicationCore.Entities;
using ApplicationCore.IRepositories;
using ApplicationCore.SharedKernel;
using Dapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DemoRepository : IDemoRepository
    {
        readonly AppDbContext _appDbContext;
        readonly AppQueryDbContext _appQueryDbContext;
        readonly IAppLogger<DemoRepository> _appLogger;
        readonly ISystemDateTime _systemDateTime;
        readonly ICache _cache;
        readonly IDbConnectionFactory _connectionFactory;

        public DemoRepository(
            AppDbContext appDbContext,
            AppQueryDbContext appQueryDbContext,
            IAppLogger<DemoRepository> appLogger,
            ISystemDateTime systemDateTime,
            ICache cache,
            IDbConnectionFactory connectionFactory)
        {
            _appDbContext = appDbContext;
            _appQueryDbContext = appQueryDbContext;
            _appLogger = appLogger;
            _systemDateTime = systemDateTime;
            _cache = cache;
            _connectionFactory = connectionFactory;
        }

        public async Task AddAsync(Demo entity)
        {
            _appDbContext.Add(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IEnumerable<DemoDto> All()
        {
            return _appQueryDbContext.Demo
                    .Select(s => new DemoDto
                    {
                        Name = s.Name,
                        Age = s.Age
                    })
                    .ToArray();
        }

        public void Delete(Guid id)
        {
            var demo = _appDbContext.Demo.Find(id);

            if (demo != null)
            {
                _appDbContext.Remove(demo);
                _appDbContext.SaveChanges();
            }
            _cache.Remove(id);
        }

        public Demo FindByKey(Guid id) => _appQueryDbContext.Demo.Find(id);

        public Demo FindByKeyOnCache(Guid id)
        {
            if (_cache.Get(id, out Demo value)) return value;
            var demo = FindByKey(id);
            _cache.Set(id, demo, new TimeSpan(0, 3, 0));
            return demo;
        }

        public PageDto<DemoDto> GetPage(int pageIndex, int pageSize, int? age, string name)
        {
            var data = _appQueryDbContext.Demo.AsNoTracking();

            data = data.IfNotNull(name, q => q.Where(w => w.Name == name));
            data = data.IfHaveValue(age, q => q.Where(w => w.Age == age));

            int total = data.Count();
            var list = data.Select(s => new DemoDto
            {
                Age = s.Age,
                Name = s.Name
            }).GetPage(pageIndex, pageSize);

            return new PageDto<DemoDto>(total, list);
        }

        public IEnumerable<Demo> GetTopRecords(int count)
        {
            using (var connection = _connectionFactory.DefaultQuery())
            {
                return connection.Query<Demo>("select top (@Count) * from Demo", new { Count = count });
            }
        }

        public void Update(Demo entity)
        {
            _appDbContext.Update(entity);
            _appDbContext.SaveChanges();
        }
    }
}