using Newtonsoft.Json;
using Oazachaosu.Core.JsonConverters;
using System;

namespace Oazachaosu.Core.Common
{
    public class ResultDTO
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { get; set; }
        [JsonProperty("groupId")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long GroupId { get; set; }
        [JsonProperty("correct")]
        public short Correct { get; set; }
        [JsonProperty("accepted")]
        public short Accepted { get; set; }
        [JsonProperty("wrong")]
        public short Wrong { get; set; }
        [JsonProperty("invisible")]
        public short Invisible { get; set; }
        [JsonProperty("timeCount")]
        public short TimeCount { get; set; }
        [JsonProperty("translationDirection")]
        public TranslationDirection TranslationDirection { get; set; }
        [JsonProperty("lessonType")]
        public LessonType LessonType { get; set; }
        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }
        [JsonProperty("state")]
        public int State { get; set; }
    }
}
