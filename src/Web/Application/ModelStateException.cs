using ApplicationCore;
using ApplicationCore.SharedKernel;
using System;

namespace Web.Application
{
    public class ModelStateException : AppException
    {
        public ModelStateException(string message) : base(message)
        {
        }

        public ModelStateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
