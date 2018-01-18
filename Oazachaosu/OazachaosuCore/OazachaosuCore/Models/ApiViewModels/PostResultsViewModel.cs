using System.Collections.Generic;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Models.ApiViewModels
{
    public class PostResultsViewModel
    {
        public string ApiKey { get; set; }
        public IEnumerable<ResultDTO> Data { get; set; }
    }
}
