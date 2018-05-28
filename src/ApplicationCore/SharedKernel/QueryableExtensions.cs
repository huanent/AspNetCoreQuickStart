using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System.Linq
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<T> GetPage<T>(this IQueryable<T> ts, int pageIndex, int pageSize)
        {
            return ts.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IQueryable<T> GetPage<T>(this IQueryable<T> ts, GetPageModel model)
        {
            return ts.GetPage(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 如果给定字段不为空或者空白字符串则执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="prop"></param>
        /// <param name="toDo"></param>
        /// <returns></returns>
        public static IQueryable<T> IfNotNull<T>(this IQueryable<T> ts, string prop, Func<IQueryable<T>, IQueryable<T>> toDo)
        {
            if (!string.IsNullOrWhiteSpace(prop)) ts = toDo(ts);
            return ts;
        }

        /// <summary>
        /// 如果给定字段有值则执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="ts"></param>
        /// <param name="prop"></param>
        /// <param name="toDo"></param>
        /// <returns></returns>
        public static IQueryable<T> IfHaveValue<T, TValue>(this IQueryable<T> ts, TValue? prop, Func<IQueryable<T>, IQueryable<T>> toDo) where TValue : struct
        {
            if (prop.HasValue) ts = toDo(ts);
            return ts;
        }
    }
}
