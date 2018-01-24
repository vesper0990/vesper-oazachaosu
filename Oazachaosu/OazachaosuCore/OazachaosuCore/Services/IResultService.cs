using System;
using System.Collections.Generic;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Services
{
    public interface IResultService : IService
    {

        IEnumerable<ResultDTO> Get(long userId, DateTime dateTime);

        void Add(ResultDTO wordDto, long userId);
        void Update(ResultDTO wordDto, long userId);

        void SaveChanges();

    }
}
