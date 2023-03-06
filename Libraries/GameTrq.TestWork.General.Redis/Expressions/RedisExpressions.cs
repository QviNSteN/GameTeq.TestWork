using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameTrq.TestWork.General.Redis.Expressions
{
    public static class RedisExpressions
    {
        public static string Serialize<T>(this T value)
        {
            if(value == null)
                return string.Empty;

            return JsonSerializer.Serialize(value);
        }

        public static T Deserialize<T>(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return default;

            return JsonSerializer.Deserialize<T>(value);
        }
    }
}
