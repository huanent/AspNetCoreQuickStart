using System;
using System.ComponentModel;

namespace MyCompany.MyProject
{
    public enum Message
    {
        /// <summary>
        /// 未找到要操作的数据！
        /// </summary>
        [Description("未找到要操作的数据！")]
        notFound,

        /// <summary>
        /// 网络错误！
        /// </summary>
        [Description("网络错误！")]
        NetError,

        /// <summary>
        /// 数据库错误！
        /// </summary>
        [Description("数据库错误！")]
        DbError,

        /// <summary>
        /// 身份验证错误！
        /// </summary>
        [Description("数据库错误！")]
        IdentityError
    }

    /// <summary>
    /// 作为程序级别异常的基类，程序异常都应该派生自此类，以便程序返回400错误时携带错误信息
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(Message message) : base(message.GetDescription())
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
