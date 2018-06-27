using Oazachaosu.Core.Common;
using System;
using System.Collections.Generic;

namespace Oazachaosu.Api.Services
{
    public interface IWordService : IService
    {

        IEnumerable<WordDTO> Get(long userId, DateTime dateTime);
        IEnumerable<WordDTO> Get(long userId, long groupId);

        void Add(WordDTO wordDto, long userId);
        long Add(WordToAddDTO wordDto, long userId);

        void Update(WordDTO wordDto, long userId);
        void Update(WordToUpdateDTO wordToUpdateDTO, long userId);

        void SaveChanges();

    }
}
