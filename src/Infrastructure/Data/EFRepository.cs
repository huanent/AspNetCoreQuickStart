using ApplicationCore;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using ApplicationCore.Exceptions;

namespace Infrastructure.Data
{

    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected DbSet<TEntity> Repository { get; }

        public EFRepository(AppDbContext dbContext) => Repository = dbContext.Set<TEntity>();

        public TEntity Find(object key) => Repository.Find(key);

        public void Add(TEntity entity) => Repository.Add(entity);

        public void Update(TEntity entity) => Repository.Update(entity);

        public void Delete(TEntity entity) => Repository.Remove(entity);

        public void AddRange(IEnumerable<TEntity> entitys) => Repository.AddRange(entitys);

        public void UpdateRange(IEnumerable<TEntity> entitys) => Repository.UpdateRange(entitys);

        public void DeleteRange(IEnumerable<TEntity> entitys) => Repository.RemoveRange(entitys);

        public IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> where = null,
            bool tracking = false,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> Include = null
            )
        {
            var query = tracking ? Repository.AsTracking() : Repository.AsNoTracking();
            if (Include != null) query = Include(query);
            if (query == null) throw new EFQueryIncludeException();
            if (where != null) query = query.Where(where);
            return query;
        }
    }
}
