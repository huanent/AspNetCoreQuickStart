using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class PageModel<T> where T : class
    {
        /// <summary>
        /// 分页数据
        /// </summary>
        public IEnumerable<T> List { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Total { get; set; }
    }
}
