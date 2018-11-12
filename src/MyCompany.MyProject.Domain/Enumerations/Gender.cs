using System;
using System.ComponentModel;

namespace MyCompany.MyProject.Domain.Enumerations
{
    public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        female = 2
    }
}
