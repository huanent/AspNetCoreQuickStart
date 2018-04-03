using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class GetDemoPageModel : GetPageModel
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
