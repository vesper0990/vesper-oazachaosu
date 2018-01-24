using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Services
{
    public interface IWordService : IService
    {

        IEnumerable<WordDTO> Get(long userId, DateTime dateTime);

        void Add(WordDTO wordDto, long userId);
        void Update(WordDTO wordDto, long userId);

        void SaveChanges();

    }
}
