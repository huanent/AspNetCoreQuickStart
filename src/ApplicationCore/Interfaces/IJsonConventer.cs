using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IJsonConventer
    {
        T ToObject<T>(string json);

        string ToJson(object obj);
    }
}
