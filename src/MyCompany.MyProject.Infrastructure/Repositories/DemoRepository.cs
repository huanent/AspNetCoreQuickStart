using Dapper;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.ApplicationCore.Dtos.Demo;
using MyCompany.MyProject.ApplicationCore.Dtos.Page;
using MyCompany.MyProject.ApplicationCore.Entities;
using MyCompany.MyProject.ApplicationCore.IRepositories;
using MyCompany.MyProject.ApplicationCore.SharedKernel;
using MyCompany.MyProject.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Infrastructure.Repositories
{
    public class DemoRepository : IDemoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAppLogger<DemoRepository> _appLogger;
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ISystemDateTime _systemDateTime;

        public DemoRepository(
            AppDbContext appDbContext,
            IAppLogger<DemoRepository> appLogger,
            ISystemDateTime systemDateTime,
            IDbConnectionFactory connectionFactory)
        {
            _appDbContext = appDbContext;
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
            return _appDbContext.Demo
                    .Select(s => new DemoDto
                    {
                        Name = s.Name,
                        Age = s.Age,
                        Info = s.DemoInfo
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

        public Demo FindByKey(Guid id) => _appDbContext.Demo.Find(id);

        public PageDto<DemoDto> GetPage(QueryDemoPageDto dto)
        {
            var data = _appDbContext.Demo.AsNoTracking();

            data = data.IfNotNull(dto.Name, q => q.Where(w => w.Name == dto.Name));
            data = data.IfHaveValue(dto.Age, q => q.Where(w => w.Age == dto.Age));

            int total = data.Count();
            var list = data.Select(s => new DemoDto
            {
                Age = s.Age,
                Name = s.Name
            }).GetPage(dto.PageIndex, dto.PageSize);

            return new PageDto<DemoDto>(total, list);
        }

        public IEnumerable<Demo> GetTopRecords(int count)
        {
            using (var connection = _connectionFactory.Default())
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
