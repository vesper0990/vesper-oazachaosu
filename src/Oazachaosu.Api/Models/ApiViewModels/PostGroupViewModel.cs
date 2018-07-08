using Oazachaosu.Core.Common;
using System.Collections.Generic;

namespace Oazachaosu.Api.Models.ApiViewModels
{
    public class PostGroupAllViewModel
    {
        public string ApiKey { get; set; }
        public IEnumerable<GroupDTO> Data { get; set; }
    }

    public class PostGroupViewModel
    {
        public string ApiKey { get; set; }
        public GroupToAddDTO Data { get; set; }
    }
}
