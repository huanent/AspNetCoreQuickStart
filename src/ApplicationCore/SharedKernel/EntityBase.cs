using System;

namespace ApplicationCore.SharedKernel
{

    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
    }
}
