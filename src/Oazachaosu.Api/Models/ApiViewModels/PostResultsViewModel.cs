using Oazachaosu.Core.Common;
using System.Collections.Generic;

namespace Oazachaosu.Api.Models.ApiViewModels
{
    public class PostResultsViewModel
    {
        public string ApiKey { get; set; }
        public IEnumerable<ResultDTO> Data { get; set; }
    }
}
