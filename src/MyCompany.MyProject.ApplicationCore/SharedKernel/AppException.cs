using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.MyProject.ApplicationCore.SharedKernel
{
    /// <summary>
    /// 作为程序级别异常的基类，程序异常都应该派生自此类，以便程序返回400错误时携带错误信息
    /// </summary>
    public class AppException : Exception
    {
        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
