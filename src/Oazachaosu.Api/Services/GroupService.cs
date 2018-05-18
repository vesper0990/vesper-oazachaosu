using AutoMapper;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Services
{
    public class GroupService : IGroupService
    {

        private readonly IWordkiRepo repository;
        private readonly IMapper mapper;

        public GroupService(IWordkiRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<GroupDTO> GetGroupsWithChildren(long userId, DateTime dateTime)
        {
            foreach (Group group in repository.GetGroups().Where(x => x.UserId == userId))
            {
                group.Words = repository.GetWords().Where(x => x.LastChange > dateTime && x.GroupId == group.Id).ToList();
                group.Results = repository.GetResults().Where(x => x.LastChange > dateTime && x.GroupId == group.Id).ToList();
                if (group.Words.Count == 0
                    && group.Results.Count == 0
                    && group.LastChange < dateTime)
                {
                    continue;
                }
                yield return mapper.Map<Group, GroupDTO>(group);
            }
        }

        public IEnumerable<GroupDTO> GetGroups(long userId, DateTime dateTime)
        {
            return mapper.Map<IEnumerable<Group>, IEnumerable<GroupDTO>>(repository.GetGroups()
            .Where(x => x.LastChange > dateTime && x.UserId == userId));
        }

        public async Task<IEnumerable<GroupItemDTO>> GetGroupItems(long userId)//todo tests
        {
            return await repository.GetGroupItems(userId);
        }

        public IQueryable<Group> GetGroups(long userId)
        {
            return repository.GetGroups().Where(x => x.UserId == userId);
        }

        public IEnumerable<Group> GetAll()
        {
            return repository.GetGroups();
        }

        public GroupDetailDTO GetGroupDetail(long userId, long groupId)
        {
            return mapper.Map<Group, GroupDetailDTO>(repository.GetGroupWithChildren(userId, groupId));
        }


        public void Update(GroupDTO groupDto, long userId)
        {
            Group group = mapper.Map<GroupDTO, Group>(groupDto);
            group.UserId = userId;
            group.LastChange = DateTime.Now;
            repository.UpdateGroup(group);
        }

        public void Add(GroupDTO groupDto, long userId)
        {
            Group group = mapper.Map<GroupDTO, Group>(groupDto);
            group.UserId = userId;
            group.LastChange = DateTime.Now;
            repository.AddGroup(group);
        }

        public void Add(GroupToCreateDTO groupDto, long userId)
        {
            Group group = new Group()
            {
                UserId = userId,
                Name = groupDto.Name,
                Language1 = groupDto.Language1Type,
                Language2 = groupDto.Language2Type,
                CreationDate = DateTime.Now,
            };
            repository.AddGroup(group);
            repository.SaveChanges();
            foreach(Word word in mapper.Map<IEnumerable<WordToCreateDTO>, IEnumerable<Word>>(groupDto.Words))
            {
                word.Id = DateTime.Now.Ticks;
                word.GroupId = group.Id;
                word.UserId = userId;
                repository.AddWord(word);
            }
            repository.SaveChanges();
        }

        public void SaveChanges()
        {
            repository.SaveChanges();
        }

    }
}
