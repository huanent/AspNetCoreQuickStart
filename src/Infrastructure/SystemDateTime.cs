using ApplicationCore.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class SystemDateTime : ISystemDateTime
    {
        /// <summary>
        /// 实现为中国标准时间
        /// </summary>
        public DateTime Now => DateTime.UtcNow.AddHours(8);
    }
}
