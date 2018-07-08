using Oazachaosu.Core.Common;
using System.Collections.Generic;

namespace Oazachaosu.Api.Models.ApiViewModels
{
    public class UpdateWordViewModel
    {
        public string ApiKey { get; set; }
        public WordDTO Data { get; set; }
    }

    public class UpdateAllViewModel
    {
        public string ApiKey { get; set; }
        public IEnumerable<WordDTO> Data { get; set; }
    }
}
