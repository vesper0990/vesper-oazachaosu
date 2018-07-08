using Newtonsoft.Json;

namespace Oazachaosu.Core.Common
{
    public class WordToAddDTO
    {
        [JsonProperty("groupId")]
        public long GroupId { get; set; }
        [JsonProperty("language1")]
        public string Language1 { get; set; }
        [JsonProperty("language2")]
        public string Language2 { get; set; }
        [JsonProperty("visibility")]
        public bool Visibility { get; set; }
        [JsonProperty("drawer")]
        public short Drawer { get; set; }
    }
}
