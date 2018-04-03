using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.IRepositories;
using ApplicationCore.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DemoRepository : IDemoRepository
    {
        readonly AppDbContext _appDbContext;
        readonly IAppLogger<DemoRepository> _appLogger;
        readonly ISystemDateTime _systemDateTime;
        readonly ICache _cache;

        public DemoRepository(
            AppDbContext appDbContext,
            IAppLogger<DemoRepository> appLogger,
            ISystemDateTime systemDateTime,
            ICache cache)
        {
            _appDbContext = appDbContext;
            _appLogger = appLogger;
            _systemDateTime = systemDateTime;
            _cache = cache;
        }

        public async Task AddAsync(DemoModel model)
        {
            var demo = model.ToDemo();
            demo.UpdateBasicInfo(_systemDateTime); //必须执行此步骤
            _appDbContext.Add(demo);
            await _appDbContext.SaveChangesAsync();
        }

        public IEnumerable<Demo> All() => _appDbContext.Demo.ToArray();

        public void Delete(Guid id)
        {
            var demo = _appDbContext.Demo.Find(id);

            _appLogger.Warn($"尝试删除Id为{id}的Demo失败，原因为未在数据库找到");
            if (demo == null) throw new AppException("未能找到要删除的Demo");

            _appDbContext.Remove(demo);
            _appDbContext.SaveChanges();
            _cache.Remove(id);
        }

        public Demo FindByKey(Guid id) => _appDbContext.Demo.Find(id);

        public Demo FindByKeyOnCache(Guid id)
        {
            if (_cache.Get(id, out Demo value)) return value;
            var demo = FindByKey(id);
            _cache.Set(id, demo);
            return demo;
        }

        public PageModel<Demo> GetPage(GetDemoPageModel model)
        {
            var data = _appDbContext.Demo.AsNoTracking();

            data.IfNotNull(model.Name, q => q.Where(w => w.Name == model.Name));
            data.IfHaveValue(model.Age, q => q.Where(w => w.Age == model.Age));

            int total = data.Count();
            var list = data.GetPage(model);
            return new PageModel<Demo>(total, list);
        }

        public IEnumerable<Demo> GetTopRecords(int count, IDbTransaction dbTransaction = null)
        {
            var connection = _appDbContext.Database.GetDbConnection();
            return connection.Query<Demo>("select top (@Count) * from Demo", new { Count = count }, dbTransaction);
        }

        public void Save(DemoModel model, Guid id)
        {
            var entity = FindByKey(id);
            if (entity == null) throw new AppException("未能找到要删除的对象");

            entity.Update(model.Name);
            _appDbContext.Update(entity);
            _appDbContext.SaveChanges();
            _cache.Remove(id);
        }
    }
}