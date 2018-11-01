using System;
using MyCompany.MyProject.Common;

namespace MyCompany.MyProject.Entities
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
        /// 在进行实体持久化时，请显示调用此方法
        /// </summary>
        public void UpdateBasicInfo()
        {
            if (CreateDate == default(DateTime)) CreateDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }
    }
}
