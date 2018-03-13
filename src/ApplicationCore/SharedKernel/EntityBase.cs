using System;

namespace ApplicationCore.SharedKernel
{

    public abstract class EntityBase<T> where T : IEquatable<T>
    {
        public T Id { get; set; }

    }

    public abstract class EntityBase : EntityBase<Guid>
    {
        /// <summary>
        /// 如果Id是空的则创建（空的Id，EF在进行持久化时是会自动创建Id的所以一般是不需要调用此方法，除非你希望提前知道Id的值）
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Guid CreateIdWhenIsEmpty(ISequenceGuid guid)
        {
            if (Id == Guid.Empty) Id = guid.Id;
            return Id;
        }
    }
}
