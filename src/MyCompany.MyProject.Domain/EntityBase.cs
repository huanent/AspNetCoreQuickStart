using System;

namespace MyCompany.MyProject.Domain
{
    public abstract class EntityBase<T> where T : struct
    {
        public T Id { get; set; }
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
        public void UpdateBasicInfo(IDatetime datetime)
        {
            if (CreateDate == default(DateTime)) CreateDate = datetime.Now;
            ModifiedDate = datetime.Now;
        }
    }
}
