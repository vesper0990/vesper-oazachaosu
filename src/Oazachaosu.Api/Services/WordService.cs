using AutoMapper;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oazachaosu.Api.Services
{
    public class WordService : IWordService
    {

        private readonly IWordkiRepo repository;
        private readonly IMapper mapper;

        public WordService(IWordkiRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<WordDTO> Get(long userId, DateTime dateTime)
        {
            return mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(repository.GetWords().Where(x => x.UserId == userId && x.LastChange > dateTime));
        }

        public void Add(WordDTO wordDto, long userId)
        {
            Word word = mapper.Map<WordDTO, Word>(wordDto);
            word.UserId = userId;
            word.LastChange = DateTime.Now;
            repository.AddWord(word);
        }

        public void Update(WordDTO wordDto, long userId)
        {
            Word word = mapper.Map<WordDTO, Word>(wordDto);
            word.UserId = userId;
            word.LastChange = DateTime.Now;
            repository.UpdateWord(word);
        }

        public void SaveChanges()
        {
            repository.SaveChanges();
        }
    }
}
