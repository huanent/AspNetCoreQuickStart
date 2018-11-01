namespace MyCompany.MyProject.Dtos.Page
{
    public class QueryPageDto
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
