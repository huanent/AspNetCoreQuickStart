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
        /// <param name="page"></param>
        /// <returns></returns>
        public static IQueryable<T> GetPage<T>(this IQueryable<T> ts, (int index, int size) page)
        {
            return GetPage(ts, page.index, page.size);
        }
    }
}
