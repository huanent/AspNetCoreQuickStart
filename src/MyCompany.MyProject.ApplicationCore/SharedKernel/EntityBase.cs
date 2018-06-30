using System;

namespace MyCompany.MyProject.ApplicationCore.SharedKernel
{

    public abstract class EntityBase<T> where T : struct
    {
        public T Id { get; protected set; }

    }

    public abstract class EntityBase : EntityBase<Guid>
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedDate { get; private set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public byte[] Timestamp { get; set; }

        /// <summary>
        /// 如果Id是空的则创建（如果Id为空，EF在进行持久化时会自动创建Id的，所以一般是【不需要】调用此方法，除非你希望提前知道Id）
        /// </summary>
        /// <param name="guid">可排序Guid</param>
        /// <returns></returns>
        public Guid CreateIdWhenIsEmpty(ISequenceGuid guid)
        {
            if (Id == Guid.Empty) Id = guid.Id;
            return Id;
        }

        /// <summary>
        /// 在进行实体持久化时，请显示调用此方法
        /// </summary>
        public void UpdateBasicInfo(ISystemDateTime systemDateTime)
        {
            if (CreateDate == default(DateTime)) CreateDate = systemDateTime.Now;
            ModifiedDate = systemDateTime.Now;
        }
    }
}
