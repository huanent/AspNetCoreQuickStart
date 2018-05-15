using ApplicationCore;
using System;

namespace Infrastructure.Implements
{
    public class SystemDateTime : ISystemDateTime
    {
        /// <summary>
        /// 实现为中国标准时间
        /// </summary>
        public DateTime Now => DateTime.UtcNow.AddHours(8);
    }
}
