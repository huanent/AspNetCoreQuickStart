using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Application.Dtos.Demo;
using MyCompany.MyProject.Application.Entities.DemoAggregate;
using MyCompany.MyProject.Application.Repositories;

namespace MyCompany.MyProject.Infrastructure.Repository
{
    [InjectScoped(typeof(IDemoRepository))]
    internal class DemoRepository : IDemoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAppSession _appSession;

        public DemoRepository(AppDbContext appDbContext, IAppSession appSession)
        {
            _appDbContext = appDbContext;
            _appSession = appSession;
        }

        public async Task AddAsync(Demo demo)
        {
            _appDbContext.Add(demo);
            await _appDbContext.SaveChangesAsync(_appSession.CancellationToken);
        }

        public async Task DeleteAsync(Demo entity)
        {
            _appDbContext.Demo.Remove(entity);
            await _appDbContext.SaveChangesAsync(_appSession.CancellationToken);
        }

        public async Task<Demo> GetByKeyAsync(Guid id)
        {
            return await _appDbContext.Demo
                .FindAsync(new object[] { id }, cancellationToken: _appSession.CancellationToken);
        }

        public async Task<DemoDto> GetEditViewAsync(Guid id)
        {
            return await _appDbContext.Demo.Select(s => new DemoDto
            {
                CreateDate = s.CreateDate,
                Id = s.Id,
                ModifiedDate = s.ModifiedDate,
                Name = s.Name,
                Items = s.DemoItems.Select(ss => new DemoDto.Item
                {
                    Name = ss.Name,
                    Sort = ss.Sort
                })
            }).FirstOrDefaultAsync(_appSession.CancellationToken);
        }

        public async Task<PageDto<DemoDto>> GetPageAsync(GetDemoPageDto dto)
        {
            var data = _appDbContext.Demo.AsNoTracking();

            if (dto.StartDate.HasValue)
            {
                var startDate = dto.StartDate?.Date;
                data = data.Where(w => w.CreateDate > startDate);
            }

            if (dto.StartDate.HasValue)
            {
                var endDate = dto.EndDate?.NextDayDate();
                data = data.Where(w => w.CreateDate > endDate);
            }

            var count = await data.CountAsync(_appSession.CancellationToken);

            var list = await data.GetPage(dto.Page()).Select(s => new DemoDto
            {
                CreateDate = s.CreateDate,
                Id = s.Id,
                ModifiedDate = s.ModifiedDate,
                Name = s.Name,
                Items = s.DemoItems.Select(ss => new DemoDto.Item
                {
                    Name = ss.Name,
                    Sort = ss.Sort
                })
            }).ToArrayAsync(_appSession.CancellationToken);

            return new PageDto<DemoDto>(count, list);
        }

        public async Task UpdateAsync(Demo demo)
        {
            _appDbContext.Update(demo);
            await _appDbContext.SaveChangesAsync(_appSession.CancellationToken);
        }
    }
}
