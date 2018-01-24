using Oazachaosu.Core.Common;
using System;
using System.Collections.Generic;

namespace Oazachaosu.Api.Services
{
    public interface IWordService : IService
    {

        IEnumerable<WordDTO> Get(long userId, DateTime dateTime);

        void Add(WordDTO wordDto, long userId);
        void Update(WordDTO wordDto, long userId);

        void SaveChanges();

    }
}
