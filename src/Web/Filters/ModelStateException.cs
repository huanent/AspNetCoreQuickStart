using ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Filters
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
