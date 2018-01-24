using Repository;
using System;
using System.Collections.Generic;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Services
{
    public interface IGroupService : IService
    {

        IEnumerable<GroupDTO> GetGroups(long userId, DateTime dateTime);
        IEnumerable<Group> GetGroups(long userId);
        IEnumerable<Group> GetAll();

        void Update(GroupDTO group, long userId);
        void Add(GroupDTO group, long userId);

        void SaveChanges();
    }
}
