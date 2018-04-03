using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    /// <summary>
    /// 缓存访问器
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 尝试从缓存获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>成功获取返回true，失败返回false</returns>
        bool Get<T>(object key, out T value);

        /// <summary>
        /// 设置一个缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="offset">过期时间偏移</param>
        void Set(object key, object value, TimeSpan? offset = null);

        /// <summary>
        /// 移除指定键的值
        /// </summary>
        /// <param name="key"></param>
        void Remove(object key);
    }
}