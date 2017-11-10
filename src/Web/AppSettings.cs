using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web
{
    public class AppSettings
    {
        /// <summary>
        /// 密码加盐
        /// </summary>
        public string UserPwdSalt { get; private set; }

        /// <summary>
        /// 超级管理员账户
        /// </summary>
        public string SuperAdminUserName { get; private set; }
    }
}
