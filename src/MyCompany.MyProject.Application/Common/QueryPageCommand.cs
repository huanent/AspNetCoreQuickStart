using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MyCompany.MyProject.Application
{
    public class QueryPageCommand<T> : IRequest<T>
    {
        /// <summary>
        /// 当前页数(从1开始)
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Index { get; set; }

        /// <summary>
        /// 分页条数
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Size { get; set; }
    }
}
