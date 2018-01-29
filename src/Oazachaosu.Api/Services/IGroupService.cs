using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oazachaosu.Api.Services
{
    public interface IGroupService : IService
    {

        IEnumerable<GroupDTO> GetGroups(long userId, DateTime dateTime);
        IEnumerable<GroupDTO> GetGroupsWithChildren(long userId, DateTime dateTime);
        IQueryable<Group> GetGroups(long userId);
        IEnumerable<Group> GetAll();

        void Update(GroupDTO group, long userId);
        void Add(GroupDTO group, long userId);

        void SaveChanges();
    }
}
