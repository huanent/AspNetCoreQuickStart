using System;
using System.ComponentModel;

namespace MyCompany.MyProject
{
    /// <summary>
    /// 作为程序级别异常的基类，程序异常都应该派生自此类，以便程序返回400错误时携带错误信息
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
