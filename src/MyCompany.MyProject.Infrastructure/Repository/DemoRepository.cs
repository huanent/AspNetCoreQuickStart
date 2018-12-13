using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public async Task UpdateAsync(Demo demo)
        {
            _appDbContext.Update(demo);
            await _appDbContext.SaveChangesAsync(_appSession.CancellationToken);
        }
    }
}
