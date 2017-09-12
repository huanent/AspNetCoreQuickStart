using System;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="T">主键数据类型</typeparam>
    public abstract class Entity<T> where T : IEquatable<T>
    {
        public T Id { get; set; }
    }

    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class Entity : Entity<Guid>
    {
    }
}
