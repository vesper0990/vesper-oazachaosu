using Oazachaosu.Core.Common;
using System.Collections.Generic;

namespace Oazachaosu.Api.Models.ApiViewModels
{
    public class PostWordsViewModel
    {
        public string ApiKey { get; set; }
        public IEnumerable<WordDTO> Data { get; set; }
    }
}
