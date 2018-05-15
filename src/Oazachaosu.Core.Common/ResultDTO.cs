using Newtonsoft.Json;
using Oazachaosu.Core.JsonConverters;
using System;

namespace Oazachaosu.Core.Common
{
    public class ResultDTO
    {
        [JsonProperty("Id")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { get; set; }
        [JsonProperty("Gid")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long GroupId { get; set; }
        [JsonProperty("C")]
        public short Correct { get; set; }
        [JsonProperty("A")]
        public short Accepted { get; set; }
        [JsonProperty("W")]
        public short Wrong { get; set; }
        [JsonProperty("Iv")]
        public short Invisible { get; set; }
        [JsonProperty("TC")]
        public short TimeCount { get; set; }
        [JsonProperty("TD")]
        public TranslationDirection TranslationDirection { get; set; }
        [JsonProperty("LT")]
        public LessonType LessonType { get; set; }
        [JsonProperty("DT")]
        public DateTime DateTime { get; set; }
        [JsonProperty("S")]
        public int State { get; set; }
    }
}
