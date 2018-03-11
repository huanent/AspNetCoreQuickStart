using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Exceptions
{
    public class NotFoundConnectionStringException : Exception
    {
        public NotFoundConnectionStringException(string message) : base(message)
        {
        }
    }
}
