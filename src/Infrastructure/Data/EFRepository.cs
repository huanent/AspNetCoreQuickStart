using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Data
{

    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected DbSet<TEntity> Repository { get; }

        public EFRepository(AppDbContext dbContext) => Repository = dbContext.Set<TEntity>();

        public TEntity Find(object key) => Repository.Find(key);

        public IQueryable<TEntity> Query(List<Expression<Func<TEntity, object>>> includes = null, bool tracking = false)
        {
            var queryable = tracking ? Repository.AsTracking() : Repository.AsNoTracking();

            if (includes != null) queryable = includes.Aggregate(
                   Repository.AsQueryable(),
                   (current, include) => current.Include(include)
                   );

            return queryable;
        }

        public void Add(TEntity entity) => Repository.Add(entity);

        public void Update(TEntity entity) => Repository.Update(entity);

        public void Delete(TEntity entity) => Repository.Remove(entity);

        public void AddRange(IEnumerable<TEntity> entitys) => Repository.AddRange(entitys);

        public void UpdateRange(IEnumerable<TEntity> entitys) => Repository.UpdateRange(entitys);

        public void DeleteRange(IEnumerable<TEntity> entitys) => Repository.RemoveRange(entitys);

    }
}
