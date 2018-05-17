﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Oazachaosu.Core.Common
{
    public class GroupToCreateDTO
    {

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("language1")]
        public LanguageType Language1Type { get; set; }
        [JsonProperty("language2")]
        public LanguageType Language2Type { get; set; }
        [JsonProperty("words")]
        public IEnumerable<WordToCreateDTO> Words { get; set; }

    }
}
