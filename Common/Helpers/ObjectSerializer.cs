using MoMo.Common.Core.Events;
using System.Text.Json;
using System.Text;

namespace MoMo.Common.Helpers
{
    public static class ObjectSerializer
    {
        public static byte[] ToMessagePayload(object obj) => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));

        public static T? ToObject<T>(byte[] bytes) => JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(bytes));
        
    }
}
