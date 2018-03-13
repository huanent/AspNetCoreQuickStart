using System;

namespace ApplicationCore.SharedKernel
{

    public abstract class EntityBase<T> where T : IEquatable<T>
    {
        public T Id { get; set; }
    }

    public abstract class EntityBase : EntityBase<Guid>
    {
        public EntityBase()
        {
            Id = SequenceGuidGenerator.SqlServerKey();
        }
    }
}
