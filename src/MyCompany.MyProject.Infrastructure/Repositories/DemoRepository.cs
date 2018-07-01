using MyCompany.MyProject.ApplicationCore.Dtos;
using MyCompany.MyProject.ApplicationCore.Dtos.Common;
using MyCompany.MyProject.ApplicationCore.Entities;
using MyCompany.MyProject.ApplicationCore.IRepositories;
using MyCompany.MyProject.ApplicationCore.SharedKernel;
using Dapper;
using MyCompany.MyProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Infrastructure.Repositories
{
    public class DemoRepository : IDemoRepository
    {
        readonly AppDbContext _appDbContext;
        readonly AppQueryDbContext _appQueryDbContext;
        readonly IAppLogger<DemoRepository> _appLogger;
        readonly ISystemDateTime _systemDateTime;
        readonly IDbConnectionFactory _connectionFactory;

        public DemoRepository(
            AppDbContext appDbContext,
            AppQueryDbContext appQueryDbContext,
            IAppLogger<DemoRepository> appLogger,
            ISystemDateTime systemDateTime,
            IDbConnectionFactory connectionFactory)
        {
            _appDbContext = appDbContext;
            _appQueryDbContext = appQueryDbContext;
            _appLogger = appLogger;
            _systemDateTime = systemDateTime;
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
        }

        public Demo FindByKey(Guid id) => _appQueryDbContext.Demo.Find(id);

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