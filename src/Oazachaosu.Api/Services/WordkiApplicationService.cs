using AutoMapper;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Services
{
    public class WordkiApplicationService : IWordkiApplicationService
    {

        private readonly IMapper mapper;
        private readonly IWordkiVersionRepo repo;

        public WordkiApplicationService(IMapper mapper, IWordkiVersionRepo repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        public IEnumerable<WordkiVersionDTO> GetAll() 
            => mapper.Map<IEnumerable<WordkiVersionDTO>>(repo.GetAll());

        public WordkiVersionDTO GetLast()
            => mapper.Map<WordkiVersionDTO>(repo.GetLast());

        public async Task Add(WordkiVersionDTO versionDto)
        {
            await repo.Add(mapper.Map<WordkiVersion>(versionDto));
        }

        public void Remove(WordkiVersionDTO versionDto)
        {
            WordkiVersion versionToRemove = repo.GetAll().SingleOrDefault(x => x.Version == versionDto.Version);
            repo.Remove(versionToRemove);
        }

    }
}
