using MyCompany.MyProject.ApplicationCore.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.ApplicationCore.Dtos
{
    public class QueryDemoPageDto : QueryPageDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }
    }
}
