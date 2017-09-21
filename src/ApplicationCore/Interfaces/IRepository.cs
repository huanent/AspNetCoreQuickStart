using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Find(object key);

        IQueryable<TEntity> Query(
            List<Expression<Func<TEntity, object>>> includes = null,
            bool tracking = false
            );

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entitys);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entitys);

        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entitys);

    }
}
