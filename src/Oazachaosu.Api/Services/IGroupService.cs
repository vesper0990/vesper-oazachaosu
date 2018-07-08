using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Services
{
    public interface IGroupService : IService
    {

        IEnumerable<GroupDTO> GetGroups(long userId, DateTime dateTime);
        IEnumerable<GroupDTO> GetGroupsWithChildren(long userId, DateTime dateTime);
        IEnumerable<GroupItemDTO> GetGroupItems(long userId);
        IQueryable<Group> GetGroups(long userId);
        IEnumerable<Group> GetAll();
        GroupDetailDTO GetGroupDetail(long userId, long groupId);

        void Update(GroupDTO group, long userId);

        void Add(GroupDTO group, long userId);
        Group Add(GroupToAddDTO group, long userId);

        Group Edit(GroupToEditDTO group, long userId);
        

        void SaveChanges();
    }
}
