using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.Dtos
{
    public class PageDto<T> where T : class
    {
        public PageDto(int total, IEnumerable<T> list)
        {
            Total = total;
            List = list ?? throw new ArgumentNullException(nameof(list));
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        public IEnumerable<T> List { get; protected set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Total { get; protected set; }
    }
}
