using Newtonsoft.Json;

namespace MyCompany.MyProject.Infrastructure
{
    public class Json : IJson
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
