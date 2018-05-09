using System.Collections.Generic;

namespace Oazachaosu.Core.Common
{
    public class GroupDetailDTO
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public LanguageType Language1 { get; set; }
        public LanguageType Language2 { get; set; }
        public IEnumerable<WordDTO> Words { get; set; }
        public IEnumerable<ResultDTO> Results { get; set; }

    }
}
