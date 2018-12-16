using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject.Core.Exceptions
{
    public class NotFoundEntityException : BadRequestException
    {
        public NotFoundEntityException(string entityName = "")
            : base($"未能找到要操作的{entityName}数据")
        {
        }
    }
}
