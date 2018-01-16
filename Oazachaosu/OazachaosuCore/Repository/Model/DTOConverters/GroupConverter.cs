using WordkiModelCore.DTO;

namespace Repository.Model.DTOConverters
{
    public static class GroupConverter
    {

        public static Group GetGroupFromDTO(GroupDTO group)
        {
            return new Group()
            {
                Id = group.Id,
                UserId = group.UserId,
                Name = group.Name,
                Language1 = group.Language1,
                Language2 = group.Language2,
                State = group.State,
                CreationDate = group.CreationDate,
            };
        }

        public static GroupDTO GetDTOFromModel(Group group)
        {
            return new GroupDTO()
            {
                Id = group.Id,
                UserId = group.UserId,
                Name = group.Name,
                Language1 = group.Language1,
                Language2 = group.Language2,
                State = group.State,
                CreationDate = group.CreationDate,
            };
        }

    }
}
