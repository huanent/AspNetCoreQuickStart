using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    public interface IJsonConventer
    {
        T ToObject<T>(string json);

        string ToJson(object obj);
    }
}
