using ApplicationCore.Entities;
using ApplicationCore.IRepositories;
using ApplicationCore.Models;
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

        public async Task AddAsync(DemoModel model)
        {
            var demo = model.ToDemo();
            demo.UpdateBasicInfo(_systemDateTime); //必须执行此步骤
            _appDbContext.Add(demo);
            await _appDbContext.SaveChangesAsync();
        }

        public IEnumerable<Demo> All() => _appQueryDbContext.Demo.ToArray();

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
            _cache.Set(id, demo,new TimeSpan(0,3,0));
            return demo;
        }

        public PageModel<Demo> GetPage(GetDemoPageModel model)
        {
            var data = _appQueryDbContext.Demo.AsNoTracking();

            data.IfNotNull(model.Name, q => q.Where(w => w.Name == model.Name));
            data.IfHaveValue(model.Age, q => q.Where(w => w.Age == model.Age));

            int total = data.Count();
            var list = data.GetPage(model);
            return new PageModel<Demo>(total, list);
        }

        public IEnumerable<Demo> GetTopRecords(int count)
        {
            using (var connection = _connectionFactory.DefaultQuery())
            {
                return connection.Query<Demo>("select top (@Count) * from Demo", new { Count = count });
            }
        }

        public void Save(DemoModel model, Guid id)
        {
            var entity = _appDbContext.Demo.Find(id);
            if (entity == null) throw new AppException("未能找到要修改的对象");

            entity.Update(model.Name);
            _appDbContext.Update(entity);
            _appDbContext.SaveChanges();
            _cache.Remove(id);
        }
    }
}