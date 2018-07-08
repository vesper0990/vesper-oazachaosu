using Newtonsoft.Json;
using Oazachaosu.Core.JsonConverters;
using System;

namespace Oazachaosu.Core.Common
{
    public class GroupItemDTO
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("language1")]
        public LanguageType Language1 { get; set; }
        [JsonProperty("language2")]
        public LanguageType Language2 { get; set; }
        [JsonProperty("wordsCount")]
        public int WordsCount { get; set; }
        [JsonProperty("resultsCount")]
        public int ResultsCount { get; set; }
        [JsonProperty("lastResultDate")]
        public DateTime? LastResultDate { get; set; }

    }
}
