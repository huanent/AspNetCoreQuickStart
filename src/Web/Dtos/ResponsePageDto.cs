using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Dtos
{
    public class ResponsePageDto<T> where T : new()
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
