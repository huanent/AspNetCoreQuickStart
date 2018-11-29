using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.MyProject
{
    public interface IJson
    {
        string ToJson(object o);

        T ToObject<T>(string json);
    }
}
