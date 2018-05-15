using ApplicationCore;
using Newtonsoft.Json;
using System;

namespace Infrastructure.Implements
{
    public class JsonConventer : IJsonConventer
    {
        public JsonConventer()
        {
        }

        public string ToJson(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("null不能进行序列化");
            }

            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                throw new Exception($"要进行序列化的对象类型为{obj.GetType()}", e);
            }
        }

        public T ToObject<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException("null或者空字符串无法进行序列化", nameof(json));
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                throw new Exception($"json转对象失败！json详情：{json}", e);
            }
        }
    }
}
