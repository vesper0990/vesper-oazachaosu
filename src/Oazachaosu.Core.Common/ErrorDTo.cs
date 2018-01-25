using Newtonsoft.Json;

namespace Oazachaosu.Core.Common
{
    public class ErrorDTO
    {

        [JsonProperty("EC")]
        public ErrorCode ErroCode { get; set; }
        [JsonProperty("M")]
        public string Message { get; set; }

    }
}
