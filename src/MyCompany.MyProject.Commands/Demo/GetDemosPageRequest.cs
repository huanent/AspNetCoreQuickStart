using MyCompany.MyProject.Dto.Demo;
using MyCompany.MyProject.Dtos;

namespace MyCompany.MyProject.Commands.Demo
{
    public class GetDemosPageRequest : PageRequest<PageDto<DemoDto>>
    {
        /// <summary>
        /// 年龄
        /// </summary>
        public int? MaxAge { get; set; }
    }
}
