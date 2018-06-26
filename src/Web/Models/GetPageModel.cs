using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class GetPageModel
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
    }
}
