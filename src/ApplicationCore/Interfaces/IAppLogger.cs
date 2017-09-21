using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IAppLogger<T>
    {
        void Info(string msg);

        void Warn(string msg);

        void Error(string msg, Exception exception = null);
    }
}
