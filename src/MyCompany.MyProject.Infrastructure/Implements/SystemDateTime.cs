using MyCompany.MyProject.ApplicationCore;
using MyCompany.MyProject.ApplicationCore.SharedKernel;
using System;

namespace MyCompany.MyProject.Infrastructure.Implements
{
    public class SystemDateTime : ISystemDateTime
    {
        /// <summary>
        /// 实现为中国标准时间
        /// </summary>
        public DateTime Now => DateTime.UtcNow.AddHours(8);
    }
}
