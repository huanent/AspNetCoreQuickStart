using ApplicationCore.ISharedKernel;
using System;

namespace Infrastructure.SharedKernel
{
    public class SystemDateTime : ISystemDateTime
    {
        /// <summary>
        /// 实现为中国标准时间
        /// </summary>
        public DateTime Now => DateTime.UtcNow.AddHours(8);
    }
}
