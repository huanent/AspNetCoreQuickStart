using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyCompany.MyProject
{
    public interface IAppSession
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// 是否登录成功
        /// </summary>
        bool Signed { get; }

        /// <summary>
        /// 异步取消令牌
        /// </summary>
        CancellationToken CancellationToken { get; }
    }
}
