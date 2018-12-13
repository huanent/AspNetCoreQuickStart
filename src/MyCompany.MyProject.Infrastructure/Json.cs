using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MyCompany.MyProject.Infrastructure
{
    [InjectSingleton(typeof(IJson))]
    internal class Json : IJson
    {
        public string ToJson(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
