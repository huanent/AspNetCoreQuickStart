using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    /// <summary>
    /// 系统时间时间
    /// </summary>
    public interface ISystemDateTime
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        DateTime Now { get; }
    }
}
