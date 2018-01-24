using Oazachaosu.Core.Common;
using System.Collections.Generic;

namespace OazachaosuCore.Models.ApiViewModels
{
    public class PostGroupViewModel
    {

        public string ApiKey { get; set; }
        public IEnumerable<GroupDTO> Data { get; set; }

    }
}
