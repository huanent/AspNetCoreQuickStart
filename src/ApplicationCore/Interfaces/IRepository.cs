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
        /// <summary>
        /// 根据主键查找实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Find(object key);

        /// <summary>
        /// 获得Ef查询对象，如无后续操作请及时ToArray或ToList
        /// </summary>
        /// <param name="includes">Include要查询的导航属性</param>
        /// <param name="tracking">是否跟踪查询出的对象，tracking为true时对象的修改会在saveChange后提交到数据库</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(
            List<Expression<Func<TEntity, object>>> includes = null,
            bool tracking = false
            );

        /// <summary>
        /// 添加实体到跟踪列表，此时并未调用saveChange，请在适当的地方使用IUnitOfWork对象来进行持久化
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        void Add(TEntity entity);

        /// <summary>
        /// 添加实体到跟踪列表，此时并未调用saveChange，请在适当的地方使用IUnitOfWork对象来进行持久化
        /// </summary>
        /// <param name="entitys">要添加的实体列表</param>
        void AddRange(IEnumerable<TEntity> entitys);

        /// <summary>
        /// 更新实体到跟踪列表，此时并未调用saveChange，请在适当的地方使用IUnitOfWork对象来进行持久化
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        void Update(TEntity entity);

        /// <summary>
        /// 更新实体到跟踪列表，此时并未调用saveChange，请在适当的地方使用IUnitOfWork对象来进行持久化
        /// </summary>
        /// <param name="entitys">要更新的实体列表</param>
        void UpdateRange(IEnumerable<TEntity> entitys);

        /// <summary>
        /// 从跟踪列表删除实体，此时并未调用saveChange，请在适当的地方使用IUnitOfWork对象来进行持久化
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 从跟踪列表删除实体，此时并未调用saveChange，请在适当的地方使用IUnitOfWork对象来进行持久化
        /// </summary>
        /// <param name="entitys">要删除的实体列表</param>
        void DeleteRange(IEnumerable<TEntity> entitys);

    }
}
