using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
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

        /// <summary>
        /// 获取页偏移
        /// </summary>
        /// <returns></returns>
        public int GetOffset() => (PageIndex - 1) * PageSize;
    }
}
