using Newtonsoft.Json;
using System.Collections.Generic;

namespace Oazachaosu.Core.Common
{
    public class GroupToAddDTO
    {
        [JsonProperty("N")]
        public string Name { get; set; }
        [JsonProperty("L1")]
        public LanguageType Language1Type { get; set; }
        [JsonProperty("L2")]
        public LanguageType Language2Type { get; set; }
        [JsonProperty("words")]
        public IEnumerable<WordToCreateDTO> Words { get; set; }

    }
}
