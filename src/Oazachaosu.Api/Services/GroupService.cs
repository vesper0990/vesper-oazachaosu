using AutoMapper;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<GroupDTO> GetGroups(long userId, DateTime dateTime)
        {
            return mapper.Map<IEnumerable<Group>, IEnumerable<GroupDTO>>(repository.GetGroups().Where(x => x.LastChange > dateTime && x.UserId == userId));
        }

        public IQueryable<Group> GetGroups(long userId)
        {
            return repository.GetGroups().Where(x => x.UserId == userId);
        }

        public IEnumerable<Group> GetAll()
        {
            return repository.GetGroups();
        }

        public void Update(GroupDTO groupDto, long userId)
        {
            Group group = mapper.Map<GroupDTO, Group>(groupDto);
            group.UserId = userId;
            repository.UpdateGroup(group);
        }

        public void Add(GroupDTO groupDto, long userId)
        {
            Group group = mapper.Map<GroupDTO, Group>(groupDto);
            group.UserId = userId;
            repository.AddGroup(group);
        }

        public void SaveChanges()
        {
            repository.SaveChanges();
        }

    }
}
