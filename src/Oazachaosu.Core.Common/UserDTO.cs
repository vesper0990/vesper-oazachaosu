using Newtonsoft.Json;

namespace Oazachaosu.Core.Common
{
    public class UserDTO
    {
        [JsonProperty("Id")]
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
