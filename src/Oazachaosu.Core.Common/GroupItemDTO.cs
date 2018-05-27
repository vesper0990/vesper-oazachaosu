﻿using Newtonsoft.Json;
using Oazachaosu.Core.JsonConverters;

namespace Oazachaosu.Core.Common
{
    public class GroupItemDTO
    {
        [JsonProperty("Id")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { get; set; }
        [JsonProperty("N")]
        public string Name { get; set; }
        [JsonProperty("L1")]
        public LanguageType Language1 { get; set; }
        [JsonProperty("L2")]
        public LanguageType Language2 { get; set; }
        [JsonProperty("WC")]
        public int WordsCount { get; set; }
        [JsonProperty("RC")]
        public int ResultsCount { get; set; }

    }
}
