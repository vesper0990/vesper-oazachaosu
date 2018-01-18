using System.Collections.Generic;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Models.ApiViewModels
{
    public class PostGroupViewModel
    {

        public string ApiKey { get; set; }
        public IEnumerable<GroupDTO> Data { get; set; }

    }
}
