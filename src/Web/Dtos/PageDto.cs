using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Dtos
{
    /// <summary>
    /// 希望查询分页可继承此Dto
    /// </summary>
    public class PageDto
    {
        /// <summary>
        /// 当前页数(从1开始)
        /// </summary>
        [Required, Range(1, int.MaxValue)]
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页条数
        /// </summary>
        [Required, Range(1, int.MaxValue)]
        public int PageSize { get; set; }

        public int Offset => (PageIndex - 1) * PageSize;
    }
}
