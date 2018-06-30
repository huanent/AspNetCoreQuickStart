using System;
using System.Threading;

namespace MyCompany.MyProject.ApplicationCore.SharedKernel
{
    /// <summary>
    /// 生成一个可排序的guid，在必要时可以在实体保存前预先生成实体Id
    /// </summary>
    public interface ISequenceGuidGenerator
    {

        /// <summary>
        /// 生成sqlserver的可排序Guid主键
        /// </summary>
        /// <returns></returns>
        ISequenceGuid SqlServerKey();

        /// <summary>
        /// 生成MySql的可排序Guid主键
        /// </summary>
        /// <returns></returns>
        ISequenceGuid MySqlKey(bool oldGuids);
    }

    /// <summary>
    /// 可排序Guid容器
    /// </summary>
    public interface ISequenceGuid
    {
        /// <summary>
        /// 可排序Guid
        /// </summary>
        Guid Id { get; }
    }
}