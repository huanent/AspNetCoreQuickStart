using System;

namespace ApplicationCore.Entities
{

    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
