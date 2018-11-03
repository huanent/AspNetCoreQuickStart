using MediatR;

namespace MyCompany.MyProject.Commands
{
    public class PageRequest<T> : IRequest<T>
    {
        /// <summary>
        /// 当前页数(从1开始)
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页条数
        /// </summary>
        public int PageSize { get; set; }
    }
}
