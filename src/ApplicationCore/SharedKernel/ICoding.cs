using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.SharedKernel
{
    public interface ICoding
    {
        string MD5Encoding(string source, string salt = default(string));
    }
}
