using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    /// <summary>
    /// 自定义异常范例
    /// </summary>
    public class CustomException : AppException
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}
