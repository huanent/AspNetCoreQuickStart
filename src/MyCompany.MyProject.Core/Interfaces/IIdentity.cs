using System;

namespace MyCompany.MyProject
{
    public interface IIdentity<T> where T : struct
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        T Id { get; }

        /// <summary>
        /// 是否已经登录
        /// </summary>
        bool IsAuth { get; }
    }

    public interface IIdentity : IIdentity<Guid>
    {
    }
}
