using Newtonsoft.Json;
using Oazachaosu.Core.JsonConverters;
using System;

namespace Oazachaosu.Core.Common
{
    public class WordDTO
    {
        [JsonConverter(typeof(LongToStringConverter))]
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonConverter(typeof(LongToStringConverter))]
        [JsonProperty("groupId")]
        public long GroupId { get; set; }
        [JsonProperty("language1")]
        public string Language1 { get; set; }
        [JsonProperty("language2")]
        public string Language2 { get; set; }
        [JsonProperty("language1Comment")]
        public string Language1Comment { get; set; }
        [JsonProperty("language2Comment")]
        public string Language2Comment { get; set; }
        [JsonProperty("drawer")]
        public byte Drawer { get; set; }
        [JsonProperty("isVisible")]
        public bool IsVisible { get; set; }
        [JsonProperty("state")]
        public int State { get; set; }
        [JsonProperty("isSelected")]
        public bool IsSelected { get; set; }
        [JsonProperty("repeatingCounter")]
        public ushort RepeatingCounter { get; set; }
        [JsonProperty("lastRepeating")]
        public DateTime LastRepeating { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
