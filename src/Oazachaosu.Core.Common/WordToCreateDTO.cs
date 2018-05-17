using Newtonsoft.Json;

namespace Oazachaosu.Core.Common
{
    public class WordToCreateDTO
    {
        [JsonProperty("language1")]
        public string Language1 { get; set; }
        [JsonProperty("language2")]
        public string Language2 { get; set; }

    }
}
