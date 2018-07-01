using MyCompany.MyProject.ApplicationCore.Dtos.Page;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.ApplicationCore.Dtos.Demo
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
