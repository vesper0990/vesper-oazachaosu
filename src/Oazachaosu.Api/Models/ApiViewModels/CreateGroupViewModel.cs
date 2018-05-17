using Oazachaosu.Core.Common;

namespace Oazachaosu.Api.Models.ApiViewModels
{
    public class CreateGroupViewModel
    {
        public string ApiKey { get; set; }
        public GroupToCreateDTO Data { get; set; }
    }
}
