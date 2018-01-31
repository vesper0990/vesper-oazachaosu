using System.Collections.Generic;
using System.Threading.Tasks;
using Oazachaosu.Core.Common;

namespace Oazachaosu.Api.Services
{
    public interface IWordkiApplicationService
    {
        Task Add(WordkiVersionDTO versionDto);
        IEnumerable<WordkiVersionDTO> GetAll();
        WordkiVersionDTO GetLast();
        void Remove(WordkiVersionDTO versionDto);
    }
}