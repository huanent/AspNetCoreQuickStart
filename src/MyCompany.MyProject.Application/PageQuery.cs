using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MyCompany.MyProject.Application
{
    public class PageQuery<T> : IRequest<T>
    {
        /// <summary>
        /// 当前页数(从1开始)
        /// </summary>
        [Range(1, int.MaxValue), DefaultValue(1)]
        public int Index { get; set; }

        /// <summary>
        /// 分页条数
        /// </summary>
        [Range(1, int.MaxValue), DefaultValue(10)]
        public int Size { get; set; }

        /// <summary>
        /// 分页信息元组
        /// </summary>
        public (int index, int size) Page() => (Index, Size);
    }
}
