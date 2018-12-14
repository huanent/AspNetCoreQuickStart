using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.Application.Dtos.Demo
{
    public class DemoDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        public IEnumerable<Item> Items { get; set; }

        public class Item
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 排序
            /// </summary>
            public int Sort { get; set; }
        }
    }
}
