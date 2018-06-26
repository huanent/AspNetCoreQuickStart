using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Dtos
{
    /// <summary>
    /// 数据传输对象，无状态与行为，只作为数据载体
    /// </summary>
    public class DemoDto
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
