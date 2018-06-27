using Newtonsoft.Json;
using Oazachaosu.Core.JsonConverters;
using System.Collections.Generic;

namespace Oazachaosu.Core.Common
{
    public class GroupDetailDTO
    {
        [JsonConverter(typeof(LongToStringConverter))]
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("language1")]
        public LanguageType Language1 { get; set; }
        [JsonProperty("language2")]
        public LanguageType Language2 { get; set; }
        [JsonProperty("words")]
        public IEnumerable<WordDTO> Words { get; set; }
        [JsonProperty("results")]
        public IEnumerable<ResultDTO> Results { get; set; }

    }
}
