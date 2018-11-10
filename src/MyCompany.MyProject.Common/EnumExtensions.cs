using System;
using System.ComponentModel;
using System.Linq;

public static class EnumExtensions
{
    /// <summary>
    /// 获取枚举的Description注解值
    /// </summary>
    /// <returns></returns>
    public static string GetDescription(this Enum e)
    {
        var type = e.GetType();
        string name = Enum.GetName(type, Convert.ToInt64(e));
        object[] result = e.GetType().GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (!result.Any()) return "";
        return ((DescriptionAttribute)result[0]).Description;
    }
}
