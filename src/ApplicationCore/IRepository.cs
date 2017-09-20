using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Find(object key);

        IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> where = null,
            bool tracking = false,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> Include = null
            );

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entitys);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entitys);

        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entitys);
    }
}
