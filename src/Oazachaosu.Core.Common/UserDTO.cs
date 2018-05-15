using Newtonsoft.Json;
using Oazachaosu.Core.JsonConverters;

namespace Oazachaosu.Core.Common
{
    public class UserDTO
    {
        [JsonProperty("Id")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { get; set; }
        [JsonProperty("ApiKey")]
        public string ApiKey { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("CreationDateTime")]
        public string CreationDateTime { get; set; }
    }
}
