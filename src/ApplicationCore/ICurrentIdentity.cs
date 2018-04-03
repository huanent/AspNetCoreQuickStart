using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    /// <summary>
    /// 当前登陆用户
    /// </summary>
    public interface ICurrentIdentity
    {
        /// <summary>
        /// 是否登陆
        /// </summary>
        bool IsLogin { get; }

        /// <summary>
        /// 当前登陆用户Id
        /// </summary>
        Guid Id { get; }
    }
}
