using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Models.ApiViewModels
{
    public class PostWordsViewModel
    {
        public string ApiKey { get; set; }
        public IEnumerable<WordDTO> Data { get; set; }
    }
}
