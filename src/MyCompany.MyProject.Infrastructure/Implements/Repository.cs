using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Infrastructure.Implements
{
    [InjectScoped(typeof(IRepository<>))]
    internal class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAppSession _appSession;

        public Repository(AppDbContext appDbContext, IAppSession appSession)
        {
            _appDbContext = appDbContext;
            _appSession = appSession;
        }

        public async Task AddAsync(T entity)
        {
            await _appDbContext.AddAsync(entity, _appSession.CancellationToken);
            await _appDbContext.SaveChangesAsync(_appSession.CancellationToken);
        }

        public async Task AddRangeAsync(params T[] Entities)
        {
            await _appDbContext.AddRangeAsync(Entities, _appSession.CancellationToken);
            await _appDbContext.SaveChangesAsync(_appSession.CancellationToken);
        }

        public async Task<T> FindAsync(Guid id)
        {
            var keys = new object[] { id };
            return await _appDbContext.FindAsync<T>(keys, cancellationToken: _appSession.CancellationToken);
        }

        public async Task RemoveAsync(T entity)
        {
            _appDbContext.Remove(entity);
            await _appDbContext.SaveChangesAsync(_appSession.CancellationToken);
        }

        public async Task RemoveRangeAsync(params T[] Entities)
        {
            _appDbContext.RemoveRange(Entities);
            await _appDbContext.SaveChangesAsync(_appSession.CancellationToken);
        }

        public async Task UpdateAsync(T entity)
        {
            _appDbContext.Update(entity);
            await _appDbContext.SaveChangesAsync(_appSession.CancellationToken);
        }

        public async Task UpdateRangeAsync(params T[] Entities)
        {
            _appDbContext.UpdateRange(Entities, _appSession.CancellationToken);
            await _appDbContext.SaveChangesAsync(_appSession.CancellationToken);
        }
    }
}
