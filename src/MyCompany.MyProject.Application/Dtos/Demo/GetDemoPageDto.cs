using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.Application.Dtos.Demo
{
    public class GetDemoPageDto : GetPageDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
