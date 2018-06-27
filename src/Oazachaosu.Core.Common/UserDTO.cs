using Newtonsoft.Json;
using Oazachaosu.Core.JsonConverters;

namespace Oazachaosu.Core.Common
{
    public class UserDTO
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { get; set; }
        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("creationDateTime")]
        public string CreationDateTime { get; set; }
    }
}
