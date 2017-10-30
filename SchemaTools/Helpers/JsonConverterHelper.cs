using Newtonsoft.Json;

namespace Telegram4Net.SchemaTools.Helpers
{
    public interface IJsonConverterHelper
    {
        string Serialize<T>(T myObject);
        TResult Deserialize<TResult>(string jsonObject);
    }

    public class JsonConverterHelper : IJsonConverterHelper
    {
        public string Serialize<T>(T myObject)
        {
            return JsonConvert.SerializeObject(myObject);
        }

        public TResult Deserialize<TResult>(string jsonObject)
        {
            return JsonConvert.DeserializeObject<TResult>(jsonObject);
        }
    }
}