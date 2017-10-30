using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram4Net.SchemaTools.Models
{
    internal class Method
    {
        public int Id { get; set; }
        [JsonProperty("method")]
        public string Name { get; set; }
        public List<Param> Params { get; set; }
        public string Type { get; set; }
    }
}