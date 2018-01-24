using AutoMapper;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Services
{
    public class ResultService : IResultService
    {

        private readonly IWordkiRepo repository;
        private readonly IMapper mapper;

        public ResultService(IWordkiRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<ResultDTO> Get(long userId, DateTime dateTime)
        {
            return mapper.Map<IEnumerable<Result>, IEnumerable<ResultDTO>>(repository.GetResults().Where(x => x.UserId == userId && x.LastChange > dateTime));
        }

        public void Add(ResultDTO resultDto, long userId)
        {
            Result result = mapper.Map<ResultDTO, Result>(resultDto);
            result.UserId = userId;
            result.LastChange = DateTime.Now;
            repository.AddResult(result);
        }

        public void Update(ResultDTO resultDto, long userId)
        {
            Result result = mapper.Map<ResultDTO, Result>(resultDto);
            result.UserId = userId;
            result.LastChange = DateTime.Now;
            repository.UpdateResult(result);
        }

        public void SaveChanges()
        {
            repository.SaveChanges();
        }
    }
}
